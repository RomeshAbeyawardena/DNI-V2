using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults.Factories
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped)]
    public class SqlDbConnectionFactory : IDbConnectionFactory
    {
        private IDbConnection dbConnection;
        public IDbConnection GetDbConnection(string connectionString)
        {
            return dbConnection = new SqlConnection(connectionString);
        }

        public void Dispose()
        {
            dbConnection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
