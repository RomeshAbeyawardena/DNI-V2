using System.Collections.Generic;

namespace DNI.MigrationManager.Shared.Abstractions.Factories
{
    public interface IDbTypeDefinitionsFactory : IDictionary<string, IDbTypeDefinitions>
    {
        IDbTypeDefinitions GetDbTypeDefinitions(string name);
    }
}
