using DNI.MigrationManager.Core.Defaults;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Modules.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureMigrationManagerModuleConfiguration(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor, Action<IMigrationManagerModuleConfiguration> configure)
        {
            IMigrationManagerModuleConfiguration migrationManagerModuleConfiguration = new DefaultMigrationManagerModuleConfiguration();

            configure(migrationManagerModuleConfiguration);

            moduleConfiguration.ConfigureOptions(moduleDescriptor, migrationManagerModuleConfiguration);
            return moduleConfiguration;
        }
    }
}
