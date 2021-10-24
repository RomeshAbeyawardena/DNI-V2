using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Modules.Extensions
{
    public static class MigrationConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddModule<T>(this IModuleConfigurationBuilder builder, Action<IModuleConfiguration> configure = null)
        {
            return builder.AddModule(ModuleDescriptor.Create<T>(ModuleDescriptor.DefaultUsage), configure);
        }
    }
}
