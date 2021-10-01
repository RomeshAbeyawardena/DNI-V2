using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Modules.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds modules to be loaded and prepared in memory.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="build"></param>
        /// <returns></returns>
        public static IModuleConfigurationBuilder AddModules(this IServiceCollection services,
            Action<IModuleConfigurationBuilder> build)
        {
            var defaultModuleConfigurationBuilder = new DefaultModuleConfigurationBuilder(services);

            build(defaultModuleConfigurationBuilder);

            return defaultModuleConfigurationBuilder;
        }
    }
}
