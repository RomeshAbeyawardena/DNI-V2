using DNI.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Abstractions.Factories;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultDbTypeDefinitionsFactory : DictionaryBase<string, IDbTypeDefinitions>, IDbTypeDefinitionsFactory
    {
        public DefaultDbTypeDefinitionsFactory(IEnumerable<IKeyValuePair<string, IDbTypeDefinitions>> definitions)
        {
            this.AddRange(definitions);
        }

        public IDbTypeDefinitions GetDbTypeDefinitions(string name)
        {
            if (this.TryGetValue(name, out var dbTypeDefinitions))
            {
                return dbTypeDefinitions;
            }

            throw new NullReferenceException();
        }
    }
}
