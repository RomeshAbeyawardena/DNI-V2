using DNI.MigrationManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Data;

namespace DNI.MigrationManager.Core.Defaults
{
    public class DefaultMigrationOptions : IMigrationOptions
    {
        internal DefaultMigrationOptions(IEnumerable<Type> types, IDictionary<Type, ITableConfiguration> tableConfiguration, Func<IServiceProvider, IDbConnection> dbConnectionFactory)
        {
            Types = types;
            TableConfiguration = tableConfiguration;
            DbConnectionFactory = dbConnectionFactory;
        }

        public IEnumerable<Type> Types { get; }
        public IDictionary<Type, ITableConfiguration> TableConfiguration { get; }
        public Func<IServiceProvider, IDbConnection> DbConnectionFactory { get; private set; }
    }
}
