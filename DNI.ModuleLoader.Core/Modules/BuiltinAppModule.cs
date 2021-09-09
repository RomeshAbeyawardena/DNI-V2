using DNI.ModuleLoader.Core.Base;
using DNI.ModuleLoader.Core.Factory;
using DNI.ModuleLoader.Core.Providers;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Serializers;
using Microsoft.Extensions.DependencyInjection;
using System;
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
                .AddSingleton<IFileProvider, LocalFileProvider>()
                .AddSingleton<ISerializerFactory, SerializerFactory>()
                .AddSingleton<ISerializer, JsonSerializer>();
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
