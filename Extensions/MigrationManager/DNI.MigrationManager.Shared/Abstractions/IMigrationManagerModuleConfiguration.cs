using System;
using System.Collections.Generic;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IMigrationManagerModuleConfiguration : IDictionary<string, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions>>
    {
        IMigrationManagerModuleConfiguration AddMigration(string name, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> configure);
    }
}
