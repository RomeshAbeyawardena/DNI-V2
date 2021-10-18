using DNI.Extensions;
using DNI.Modules.Core.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly IModulesServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private readonly IModuleConfiguration moduleConfiguration;
        private ICompiledModuleConfiguration compiledModuleConfiguration;
        private IServiceProvider moduleServiceProvider;
        private readonly Dictionary<Guid, IEnumerable<IDisposable>> disposableTypesList;
        private readonly List<Action<IServiceCollection, IModuleConfiguration>> serviceConfigurations;
        private readonly List<IModule> configuredModules;
        private readonly ILogger<IModuleRunner> logger;
        
        private ICompiledModuleConfiguration ConfigureModuleConfiguration(IServiceProvider serviceProvider)
        {
            return moduleConfiguration.Compile(serviceProvider, configuredModules, logger);
        }

        private Task OnStartModule(IModule module, CancellationToken cancellationToken)
        {
            if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                module.Disposables = disposables;

            return module.StartAsync(cancellationToken);
        }

        private void ConfigureModules(IModuleConfiguration moduleConfiguration)
        {
            foreach (var module in configuredModules)
            {
                module.ConfigureServices(services, moduleConfiguration);
                if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                {
                    module.Disposables = disposables;
                }
            }
        }

        public DefaultModuleRunner(
            IServiceProvider serviceProvider,
            IModuleConfiguration moduleConfiguration)
        {
            this.services = new DefaultModulesServiceCollection();
            this.serviceProvider = serviceProvider;
            this.moduleConfiguration = moduleConfiguration;
            disposableTypesList = new Dictionary<Guid, IEnumerable<IDisposable>>();
            serviceConfigurations = new List<Action<IServiceCollection, IModuleConfiguration>>();
            configuredModules = new List<IModule>();
            logger = serviceProvider.GetRequiredService<ILogger<IModuleRunner>>();
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            logger.LogInformation("Configuring module builder...");

            var hasChanged = false;

            foreach (var moduleType in moduleConfiguration.ModuleTypes)
            {
                if (configuredModules.Any(a => a.ModuleType == moduleType))
                {
                    continue;
                }

                logger.LogInformation("Configuring module {0}...", moduleType);

                var module = serviceProvider.Activate<IModule>(moduleType, out var disposables);
                module.ConfigureModuleBuilder(services, moduleConfigurationBuilder);
                
                if (moduleConfiguration.ApplyConfiguration(moduleConfigurationBuilder))
                {
                    hasChanged = true;
                }

                logger.LogInformation("{0} assigned Id: {1}", moduleType, module.UniqueId);
                
                disposableTypesList.Add(module.UniqueId, disposables);
                configuredModules.Add(module);
            }

            if (hasChanged)
            {
                logger.LogInformation("Changes to module configuration have been made by other modules, re-running module builder configuration");
                ConfigureModuleBuilder(services, moduleConfigurationBuilder);
            }

        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            logger.LogInformation("Configuring module service registeration...");
            serviceConfigurations.ForEach(sc => sc?.Invoke(services, moduleConfiguration));

            ConfigureModules(moduleConfiguration);

            services.AddSingleton(ConfigureModuleConfiguration);
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            logger.LogInformation("Module runner starting with {0} modules", moduleConfiguration.ModuleTypes.Count());
            ConfigureModuleBuilder(services, new DefaultModuleConfigurationBuilder(moduleConfiguration));
            ConfigureServices(services, moduleConfiguration);
            logger.LogInformation("Module runner configured with {0} modules", moduleConfiguration.ModuleTypes.Count());
            moduleServiceProvider = new DefaultModuleServiceProvider(serviceProvider, services.BuildServiceProvider());
            compiledModuleConfiguration = moduleServiceProvider.GetRequiredService<ICompiledModuleConfiguration>();
            logger.LogInformation("Module runner started");
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => OnStartModule(m, cancellationToken)));
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            logger.LogInformation("Module runner stopping modules...");

            if (compiledModuleConfiguration != null)
            {
                return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => m.StopAsync(cancellationToken)));
            }

            return Task.CompletedTask;
        }

        public override void OnDispose(bool disposing)
        {
            compiledModuleConfiguration?.Modules.ForEach(m => m.Dispose());
        }

        public void AddServiceConfiguration(Action<IServiceCollection, IModuleConfiguration> configureAction)
        {
            serviceConfigurations.Add(configureAction);
        }
    }
}
