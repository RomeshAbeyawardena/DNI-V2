using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAppModuleLoader<TAppModuleLoader>(this IServiceCollection services)
            where TAppModuleLoader : class, IAppModuleLoader
        {
            return services
                .AddSingleton<IAppModuleLoader, TAppModuleLoader>();
        }

        public static IServiceCollection Requires<TAppModule>(this IServiceCollection services)
            where TAppModule : class, IAppModule
        {
            return services;
        }
    }
}
