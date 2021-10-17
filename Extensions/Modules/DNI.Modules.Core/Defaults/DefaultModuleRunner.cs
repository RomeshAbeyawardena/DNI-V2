using DNI.Extensions;
using DNI.Modules.Core.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly IModulesServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private IModuleConfiguration moduleConfiguration;
        private ICompiledModuleConfiguration compiledModuleConfiguration;
        private IServiceProvider moduleServiceProvider;
        private readonly Dictionary<Guid, IEnumerable<IDisposable>> disposableTypesList;
        private readonly List<Action<IServiceCollection, IModuleConfiguration>> serviceConfigurations;
        private readonly List<IModule> configuredModules;
        private ICompiledModuleConfiguration ConfigureModuleConfiguration(IServiceProvider serviceProvider)
        {
            return moduleConfiguration.Compile(serviceProvider, configuredModules);
        }

        private Task OnStartModule(IModule module, CancellationToken cancellationToken)
        {
            if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                module.Disposables = disposables;

            return module.StartAsync(cancellationToken);
        }

        private void ConfigureModules(IServiceProvider serviceProvider, IModuleConfiguration moduleConfiguration)
        {
            foreach (var module in configuredModules)
            {
                module.ConfigureServices(services, moduleConfiguration);
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
        }

        public override void ConfigureBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            var hasChanged = false;

            foreach (var moduleType in moduleConfiguration.ModuleTypes)
            {
                if(configuredModules.Any(a => a.ModuleType == moduleType))
                {
                    continue;
                }

                var module = serviceProvider.Activate<IModule>(moduleType, out var disposables);
                module.ConfigureBuilder(services, moduleConfigurationBuilder);
                hasChanged = moduleConfiguration.ApplyConfiguration(moduleConfigurationBuilder);
                var moduleId = Guid.NewGuid();
                module.UniqueId = moduleId;
                disposableTypesList.Add(moduleId, disposables);
                configuredModules.Add(module);
            }

            if (hasChanged)
            {
                ConfigureBuilder(services, moduleConfigurationBuilder);
            }
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            serviceConfigurations.ForEach(sc => sc?.Invoke(services, moduleConfiguration));

            var fakeServiceProvider = new DefaultFakeServiceProvider();

            ConfigureModules(fakeServiceProvider, moduleConfiguration);
            
            services.AddSingleton(ConfigureModuleConfiguration);
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            ConfigureBuilder(services, new DefaultModuleConfigurationBuilder(moduleConfiguration));
            ConfigureServices(services, moduleConfiguration);
            moduleServiceProvider = new DefaultModuleServiceProvider(serviceProvider, services.BuildServiceProvider());
            compiledModuleConfiguration = moduleServiceProvider.GetRequiredService<ICompiledModuleConfiguration>();
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => OnStartModule(m, cancellationToken)));
        }

        

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => m.StopAsync(cancellationToken)));
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
