using DNI.Hangfire.Core.Defaults.Builders;
using DNI.Hangfire.Shared.Abstractions.Builder;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Hangfire.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureHangfireModule(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor, Action<IHangfireModuleOptionsBuilder> configure)
        {
            var hangfireModuleOptionsBuilder = new DefaultHangfireModuleOptionsBuilder();

            configure(hangfireModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, hangfireModuleOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
