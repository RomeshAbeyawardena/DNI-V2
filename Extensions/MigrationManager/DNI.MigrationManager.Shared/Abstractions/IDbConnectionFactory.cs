using System;
using System.Data;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection GetDbConnection(string connectionString);
    }
}
