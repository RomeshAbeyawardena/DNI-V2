using System;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IMigrationQueryBuilder : IDisposable
    {
        string BuildMigrations(string dbType);
    }
}
