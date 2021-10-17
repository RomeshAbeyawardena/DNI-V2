using DNI.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DNI.MigrationManager.Core.Defaults
{
    [RegisterService]
    public class DefaultMigrationManager : ConcurrentQueue<IMigrationOptions>, IMigrationManager
    {
        public DefaultMigrationManager(IEnumerable<IKeyValuePair<string, IMigrationOptions>> migrationOptionPairs)
        {
            migrationOptionsDictionary = new Dictionary<string, IMigrationOptions>();
            migrationOptionsDictionary.AddRange(migrationOptionPairs);

            migrationOptionPairs
                .Select(a => a.Value)
                .ForEach(Enqueue);
        }

        private readonly Dictionary<string, IMigrationOptions> migrationOptionsDictionary;
        private bool isReadOnly;

        public ConcurrentQueue<IMigrationOptions> Migrations => this;


        public void Add(string name, IMigrationOptions migrationOptions)
        {
            if (!isReadOnly)
            {
                migrationOptionsDictionary.Add(name, migrationOptions);
            }

            throw new NotSupportedException("Migration manager is in read-only mode, it can only be used for retrieving migration options, in its current state");
        }

        public IMigrationOptions GetMigrationOptions(string name)
        {
            return migrationOptionsDictionary.GetValueOrDefault(name);
        }
    }
}
