using DNI.ModuleLoader.Core;
using DNI.ModuleLoader.Core.Base;
using DNI.Shared;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using DNI.Shared.Serializers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Modules
{
    public class BuiltinAppModule : AppModuleBase<BuiltinAppModule>
    {
        private static string[] Types => new[] { "Service", "Provider", "Factory", "Serializer", "Config" };
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
                .AddSingleton(typeof(IAppModuleConfig<>), typeof(AppModuleConfig<>))
                .Scan(scanner => scanner
                    .FromAssemblies(Core.This.Assembly, Shared.This.Assembly)
                    .AddClasses(c => c.Where(t => OfTypes(t, Types) 
                    && HasNotGotAServiceDiscoveryAttributeOrServiceDiscoveryIsEnabled(t)))
                    .AsImplementedInterfaces());
        }

        public static bool OfTypes(Type type, params string[] types)
        {
            return types.Any(t => type.Name.ToLower().EndsWith(t.ToLower()));
        }

        public static bool HasNotGotAServiceDiscoveryAttributeOrServiceDiscoveryIsEnabled(Type type)
        {
            var serviceDiscoveryAttribute = type.GetCustomAttribute<ServiceDiscoveryAttribute>();
            
            if(serviceDiscoveryAttribute == null)
            {
                return true;
            }

            return serviceDiscoveryAttribute.EnableServiceDiscovery;
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
