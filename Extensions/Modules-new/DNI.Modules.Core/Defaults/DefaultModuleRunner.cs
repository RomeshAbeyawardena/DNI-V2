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
    public class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly IModulesServiceCollection services;
        private readonly IServiceProvider serviceProvider;
        private readonly IModuleConfiguration moduleConfiguration;
        private ICompiledModuleConfiguration compiledModuleConfiguration;
        private IServiceProvider moduleServiceProvider;
        private Dictionary<Type, IEnumerable<IDisposable>> disposableTypesList;

        public DefaultModuleRunner(
            IServiceProvider serviceProvider,
            IModuleConfiguration moduleConfiguration)
        {
            this.services = new DefaultModulesServiceCollection();
            this.serviceProvider = serviceProvider;
            this.moduleConfiguration = moduleConfiguration;
            disposableTypesList = new Dictionary<Type, IEnumerable<IDisposable>>();
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            var fakeServiceProvider = new FakeServiceProvider();
            foreach(var moduleType in moduleConfiguration.ModuleTypes)
            {
                var module = fakeServiceProvider.Activate<IModule>(moduleType, out var disposables);
                
                module.ConfigureServices(services);
            }

            services.AddSingleton(ConfigureModuleConfiguration);
        }

        private ICompiledModuleConfiguration ConfigureModuleConfiguration(IServiceProvider serviceProvider)
        {
            return moduleConfiguration.Compile(serviceProvider);
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            ConfigureServices(services);
            moduleServiceProvider = new ModuleServiceProvider(serviceProvider, services.BuildServiceProvider());
            compiledModuleConfiguration = moduleServiceProvider.GetRequiredService<ICompiledModuleConfiguration>();
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => m.StartAsync(cancellationToken)));
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.WhenAll(compiledModuleConfiguration.Modules.ForEach(m => m.StopAsync(cancellationToken)));
        }

        public override void Dispose(bool disposing)
        {
            
        }
    }
}
