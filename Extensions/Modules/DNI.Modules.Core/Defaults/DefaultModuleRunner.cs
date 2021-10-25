using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal partial class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly IServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private readonly IModuleConfiguration moduleConfiguration;
        private ICompiledModuleConfiguration compiledModuleConfiguration;
        private IServiceProvider moduleServiceProvider;
        private readonly Dictionary<Guid, IEnumerable<IDisposable>> disposableTypesList;
        private readonly List<Action<IServiceCollection, IModuleConfiguration>> serviceConfigurations;
        private readonly List<IModule> configuredModules;
        private readonly ILogger<IModuleRunner> logger;
        
        public DefaultModuleRunner(
            IServiceCollection serviceCollection,
            IServiceProvider serviceProvider,
            IModuleConfiguration moduleConfiguration)
        {
            this.services = serviceCollection;
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

            var hasChanges = ConfigureModulesBuilder(services, moduleConfigurationBuilder, out var newCount);

            if (hasChanges)
            {
                logger.LogInformation("{0} changes have been detected, re-running module builder configuration", newCount);
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
            Stopwatch stopwatch = new();

            stopwatch.Start();
            logger.LogInformation("Module runner starting with {0} modules", moduleConfiguration.ModuleDescriptors.Count);
            ConfigureModuleBuilder(services, new DefaultModuleConfigurationBuilder(moduleConfiguration));
            ConfigureServices(services, moduleConfiguration);
            logger.LogInformation("Module runner configured with {0} modules", moduleConfiguration.ModuleDescriptors.Count);
            moduleServiceProvider = services.BuildServiceProvider();
            compiledModuleConfiguration = moduleServiceProvider.GetRequiredService<ICompiledModuleConfiguration>();
            moduleConfiguration.ServiceProvider = moduleServiceProvider;
            stopwatch.Stop();
            logger.LogInformation("Module runner started in {0}", stopwatch.Elapsed);

            List<Task> taskList = new();
            foreach(var module in compiledModuleConfiguration.Modules)
            {
                taskList.Add(OnStartModule(module, cancellationToken));
            }

            return Task.WhenAll(taskList);
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
