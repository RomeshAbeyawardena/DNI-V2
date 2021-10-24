using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Modules.Extensions
{
    public static class MigrationConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddModule<T>(this IModuleConfigurationBuilder builder, Action<IModuleDescriptor, IModuleConfiguration> configure = null)
        {
            var moduleDescriptor = ModuleDescriptor.Create<T>(ModuleDescriptor.DefaultUsage);
            return builder.AddModule(moduleDescriptor, configure);
        }
    }
}
