using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Web.Extensions;
using DNI.Web.Shared.Abstractions.Builders;
using System;

namespace DNI.Web.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {

        public static IModuleConfigurationBuilder ConfigureWebModule(this IModuleConfigurationBuilder builder, Type type, Action<IWebModuleOptionsBuilder> configure)
        {
            builder.AddModule<WebModule>((moduleDescriptor, cfg) => cfg.ConfigureWebModuleOptions(moduleDescriptor, type.Assembly, configure));
            return builder;
        }

        public static IModuleConfigurationBuilder ConfigureWebModule<T>(this IModuleConfigurationBuilder builder, Action<IWebModuleOptionsBuilder> configure)
        {
            return ConfigureWebModule(builder, typeof(T), configure);
        }

    }
}
