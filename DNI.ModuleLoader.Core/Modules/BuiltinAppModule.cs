using DNI.ModuleLoader.Core.Base;
using DNI.Shared;
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
    public class BuiltinAppModule : AppModuleBase<BuiltinAppModule>
    {
        private static string[] Types => new[] { "Service", "Provider", "Factory", "Serializer" };
        public BuiltinAppModule(IAppModuleCache<BuiltinAppModule> appModuleCache) : base(appModuleCache)
        {
        }

        public static bool UseGlobalAppModuleCache() => true;

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            services
                .AddSingleton(typeof(ISwitch<,>), typeof(DefaultSwitch<,>))
                .AddSingleton(typeof(IGlobalAppModuleCache<>), typeof(GlobalModuleCache<>))
                .AddSingleton(typeof(IAppModuleCache<>), typeof(AppModuleCache<>))
                .Scan(scanner => scanner
                    .FromAssemblies(This.Assembly, Shared.This.Assembly)
                    .AddClasses(c => c.Where(t => OfTypes(t, Types)))
                    .AsImplementedInterfaces());
        }

        public static bool OfTypes(Type type, params string[] types)
        {
            return types.Any(t => type.Name.ToLower().EndsWith(t.ToLower()));
        }

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("BuiltinAppModule running");
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
