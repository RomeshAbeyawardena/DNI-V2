using System;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IDbTypeDefinitions
    {
        string GetType(Type type);
        string GetType(string type);
    }
}
