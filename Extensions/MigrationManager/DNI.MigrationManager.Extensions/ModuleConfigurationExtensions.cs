using DNI.MigrationManager.Core.Defaults;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureMigrationManagerModuleConfiguration(this IModuleConfiguration moduleConfiguration, Action<IMigrationManagerModuleConfiguration> configure)
        {
            IMigrationManagerModuleConfiguration migrationManagerModuleConfiguration = new DefaultMigrationManagerModuleConfiguration();

            configure(migrationManagerModuleConfiguration);

            moduleConfiguration.ConfigureOptions(migrationManagerModuleConfiguration);
            return moduleConfiguration;
        }
    }
}
