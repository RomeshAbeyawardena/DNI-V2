using DNI.ModuleLoader.Core.Factory;
using DNI.ModuleLoader.Core.Modules;
using DNI.ModuleLoader.Core.Providers;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Serializers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Bootstrapper
{
    public static class AppModuleBootstapper
    {
        /// <summary>
        /// Bootstraps <typeparamref name="TAppModuleLoader"/> and injects basic services to obtain an instance of <see cref="IAppModuleLoader"/> of <typeparamref name="TAppModuleLoader"/>
        /// </summary>
        /// <typeparam name="TAppModuleLoader"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static TAppModuleLoader Bootstrap<TAppModuleLoader>(IServiceCollection services)
            where TAppModuleLoader : class, IAppModuleLoader
        {
            BuiltinAppModule.RegisterServices(null, services);
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetRequiredService<IAppModuleLoader>() as TAppModuleLoader;
        }
    }
}
