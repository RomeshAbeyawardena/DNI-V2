using DNI.Hangfire.Extensions;
using DNI.Hangfire.Shared.Abstractions.Builder;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Hangfire.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureHangfireModule(this IModuleConfigurationBuilder builder, Action<IHangfireModuleOptionsBuilder> configure)
        {
            return builder.AddModule<HangfireWebModule>((moduleDescriptor, configuration) => configuration.ConfigureHangfireModule(moduleDescriptor, configure));
        }
    }
}
