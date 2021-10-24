using DNI.MigrationManager.Modules;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Mediator.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureMigrationModule(this IModuleConfigurationBuilder builder, Action<IMigrationManagerModuleConfiguration> configure)
        {
            return builder.AddModule<MigrationManagerModule>((moduleDescriptor, configuration) => configuration
                .ConfigureMigrationManagerModuleConfiguration(moduleDescriptor, configure));
        }
    }
}
