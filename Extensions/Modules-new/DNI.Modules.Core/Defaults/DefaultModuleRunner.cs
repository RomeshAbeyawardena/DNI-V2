using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
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
        private ICompiledModuleConfiguration ConfigureModuleConfiguration(IServiceProvider serviceProvider)
        {
            return moduleConfiguration.Compile(serviceProvider);
        }
        private Task OnStartModule(IModule module, CancellationToken cancellationToken)
        {
            if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                module.Disposables = disposables;

            return module.StartAsync(cancellationToken);
        }

        public DefaultModuleRunner(
            IServiceProvider serviceProvider,
            IModuleConfiguration moduleConfiguration)
        {
            this.services = new DefaultModulesServiceCollection();
            this.serviceProvider = serviceProvider;
            this.moduleConfiguration = moduleConfiguration;
            disposableTypesList = new Dictionary<Guid, IEnumerable<IDisposable>>();
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var fakeServiceProvider = new DefaultFakeServiceProvider();
            foreach(var moduleType in moduleConfiguration.ModuleTypes)
            {
                var module = fakeServiceProvider.Activate<IModule>(moduleType, out var disposables);
                var moduleId = Guid.NewGuid();
                module.UniqueId = moduleId;
                disposableTypesList.Add(moduleId, disposables);
                module.ConfigureServices(services, moduleConfiguration);
            }

            services.AddSingleton(ConfigureModuleConfiguration);
        }

        

        public override Task OnStart(CancellationToken cancellationToken)
        {
            ConfigureServices(services, moduleConfiguration);
            moduleServiceProvider = new DefaultModuleServiceProvider(serviceProvider, services.BuildServiceProvider());
            compiledModuleConfiguration = moduleServiceProvider.GetRequiredService<ICompiledModuleConfiguration>();
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => OnStartModule(m, cancellationToken)));
        }

        

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => m.StopAsync(cancellationToken)));
        }

        public override void Dispose(bool disposing)
        {
            compiledModuleConfiguration?.Modules.ForEach(m => m.Dispose());
        }
    }
}
