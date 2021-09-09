using DNI.ModuleLoader.Core.Base;
using DNI.ModuleLoader.Core.Factory;
using DNI.ModuleLoader.Core.Providers;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Serializers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Modules
{
    public class BuiltinAppModule : AppModuleBase
    {
        public BuiltinAppModule(IAppModuleCache appModuleCache) : base(appModuleCache)
        {
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            services
                .AddSingleton(typeof(IAppModuleCache<>), typeof(AppModuleCache<>))
                .Scan(scanner => scanner
                    .FromAssemblies(This.Assembly, Shared.This.Assembly)
                    .AddClasses(c => c.Where(t => OfTypes(t, "Service", "Provider", "Factory", "Serializer")))
                    .AsImplementedInterfaces());
        }

        public static bool OfTypes(Type type, params string[] types)
        {
            return types.Any(t => type.Name.ToLower().EndsWith(t.ToLower()));
        }

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }
    }
}
