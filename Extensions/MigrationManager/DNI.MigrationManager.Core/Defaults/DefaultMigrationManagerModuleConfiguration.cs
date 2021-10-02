using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults
{
    public class DefaultMigrationManagerModuleConfiguration : DictionaryBase<string, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions>>, IMigrationManagerModuleConfiguration
    {
        public IMigrationManagerModuleConfiguration AddMigration(string name, Func<IServiceProvider, IMigrationConfigurator, IMigrationOptions> configure)
        {
            Add(name, configure);
            return this;
        }
    }
}
