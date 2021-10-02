using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions.Factories
{
    public interface IDbTypeDefinitionsFactory : IDictionary<string, IDbTypeDefinitions>
    {
        IDbTypeDefinitions GetDbTypeDefinitions(string name);
    }
}
