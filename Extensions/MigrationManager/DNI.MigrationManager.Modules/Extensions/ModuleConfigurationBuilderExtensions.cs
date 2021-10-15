using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Modules;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureMigrationModule(this IModuleConfigurationBuilder builder, Action<IMigrationManagerModuleConfiguration> configure)
        {
            return builder.AddModule<MigrationManagerModule>(configuration => configuration
                .ConfigureMigrationManagerModuleConfiguration(configure));
        }
    }
}
