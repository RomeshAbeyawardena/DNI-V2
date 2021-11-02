using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleStartup : ModuleBase, IModuleStartup
    {
        private readonly IModuleRunner moduleRunner;

        public DefaultModuleStartup(
            IModuleConfiguration moduleConfiguration,
            IModuleRunner moduleRunner)
        {
            Configuration = moduleConfiguration;
            this.moduleRunner = moduleRunner;
        }

        public IModuleConfiguration Configuration { get; }

        private IEnumerable<Assembly> GetAssemblies(Type type)
        {
            var requiresDependenciesAttribute = type.GetCustomAttribute<RequiresDependenciesAttribute>();

            if (requiresDependenciesAttribute == null)
            {
                return Array.Empty<Assembly>();
            }

            return requiresDependenciesAttribute.RequiredTypes.Select(t => t.Assembly).Distinct();
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var dependencies = moduleConfiguration.ModuleDescriptors.Types.SelectMany(GetAssemblies)
                .AppendMany(moduleConfiguration.ModuleDescriptors.Types.Select(a => a.Assembly))
                .Distinct();

            services.Scan(c => c.FromAssemblies(dependencies)
                .AddClasses(t => t.WithAttribute<RegisterServiceAttribute>(s => s.ServiceLifetime == ServiceLifetime.Singleton))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(t => t.WithAttribute<RegisterServiceAttribute>(s => s.ServiceLifetime == ServiceLifetime.Scoped))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(t => t.WithAttribute<RegisterServiceAttribute>(s => s.ServiceLifetime == ServiceLifetime.Transient))
                .AsImplementedInterfaces()
                .WithTransientLifetime());
        }

        public override void OnDispose(bool disposing)
        {
            moduleRunner.Dispose();
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            moduleRunner.AddServiceConfiguration(ConfigureServices);
            return moduleRunner.StartAsync(cancellationToken);
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return moduleRunner.StopAsync(cancellationToken);
        }
    }
}
