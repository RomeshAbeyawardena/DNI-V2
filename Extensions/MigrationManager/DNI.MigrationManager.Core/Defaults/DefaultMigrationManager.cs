using DNI.MigrationManager.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using System;
using System.Collections.Concurrent;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults
{
    [RegisterService]
    public class DefaultMigrationManager : ConcurrentQueue<IMigrationOptions>,  IMigrationManager
    {
        public DefaultMigrationManager(IEnumerable<IKeyValuePair<string, IMigrationOptions>> migrationOptionPairs)
        {
            migrationOptionsDictionary = new Dictionary<string, IMigrationOptions>();
            migrationOptionsDictionary.AddRange(migrationOptionPairs);
        }

        private readonly Dictionary<string, IMigrationOptions> migrationOptionsDictionary;
        private ConcurrentQueue<IMigrationOptions> migrations;
        private bool isReadOnly;
        public ConcurrentQueue<IMigrationOptions> Migrations
        {
            get
            {
                if (migrations == null)
                {
                    migrations = new ConcurrentQueue<IMigrationOptions>(migrationOptionsDictionary.ToArray().Select(a => a.Value));
                    isReadOnly = true;
                }
                return migrations;
            }
        }

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
