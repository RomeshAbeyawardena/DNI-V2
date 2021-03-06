using DNI.MigrationManager.Core.Defaults.Builders;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Abstractions.Builders;
using DNI.Shared.Attributes;
using System;
using System.Collections.Generic;

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
