using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IDbTypeDefinitions
    {
        string GetType(Type type);
        string GetType(string type);
    }
}
