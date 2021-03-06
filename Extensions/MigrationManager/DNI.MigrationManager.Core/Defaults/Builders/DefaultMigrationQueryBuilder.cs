using Dapper;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace DNI.MigrationManager.Core.Defaults.Builders
{
    [RegisterService(ServiceLifetime.Transient)]
    public class DefaultMigrationQueryBuilder : IMigrationQueryBuilder
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IDatabaseQueryBuilderFactory databaseQueryBuilderFactory;
        private readonly IMigrationManager migrationsManager;
        private IServiceScope scope;
        private IDbConnection dbConnection;
        public DefaultMigrationQueryBuilder(
            IServiceProvider serviceProvider,
            IDatabaseQueryBuilderFactory databaseQueryBuilderFactory,
            IMigrationManager migrationsManager)
        {
            this.serviceProvider = serviceProvider;
            this.databaseQueryBuilderFactory = databaseQueryBuilderFactory;
            this.migrationsManager = migrationsManager;
        }

        public string BuildMigrations(string dbType)
        {
            var queryBuilder = databaseQueryBuilderFactory.GetDatabaseQueryBuilder(dbType);
            var queryStringBuilder = new StringBuilder();
            while (migrationsManager.TryTake(out var migrationOptions))
            {
                scope = serviceProvider.CreateScope();
                dbConnection = migrationOptions.DbConnectionFactory(scope.ServiceProvider);

                dbConnection.Open();
                var tableConfigurations = migrationOptions.TableConfiguration.Select(t => t.Value);

                foreach (var tableConfiguration in tableConfigurations)
                {
                    if (!dbConnection.ExecuteScalar<bool>(queryBuilder.TableExists(tableConfiguration)))
                    {
                        queryStringBuilder.AppendLine(queryBuilder.CreateTable(tableConfiguration, tableConfiguration.DataColumns));
                    }
                    else
                    {
                        foreach (var column in tableConfiguration.DataColumns)
                        {
                            if (!dbConnection.ExecuteScalar<bool>(queryBuilder.ColumnExists(tableConfiguration, column.Name)))
                            {
                                queryStringBuilder.AppendLine(queryBuilder.CreateField(tableConfiguration, column));
                            }
                        }
                    }
                }

            }

            return queryStringBuilder.ToString();
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
            scope?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
