using DNI.Hangfire.Core.Defaults.Builders;
using DNI.Hangfire.Shared.Abstractions.Builder;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Hangfire.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureHangfireModuleOptions(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor, Type parentType, Action<IHangfireModuleOptionsBuilder> configure)
        {
            var hangfireModuleOptionsBuilder = new DefaultHangfireModuleOptionsBuilder();
            hangfireModuleOptionsBuilder.SetParentType(parentType);
            configure(hangfireModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, hangfireModuleOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
