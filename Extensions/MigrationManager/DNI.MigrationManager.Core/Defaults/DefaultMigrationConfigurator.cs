using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults
{
    /// <inheritdoc cref="IMigrationConfigurator" />
    /// 
    [RegisterService]
    public class DefaultMigrationConfigurator : IMigrationConfigurator
    {
        private readonly IMigrationConfiguratorOptionsBuilder builder;

        public DefaultMigrationConfigurator(IServiceProvider serviceProvider)
        {
            builder = new DefaultMigrationConfiguratorOptionsBuilder(serviceProvider, new List<Type>(), new Dictionary<Type, ITableConfiguration>());
        }

        public IMigrationOptions Build()
        {
            return builder.Build();
        }

        public IMigrationConfigurator Configure(Action<IMigrationConfiguratorOptionsBuilder> configure)
        {
            configure(builder);
            return this;
        }
    }
}
