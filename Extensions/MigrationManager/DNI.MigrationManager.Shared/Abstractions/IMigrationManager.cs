using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IMigrationManager : IProducerConsumerCollection<IMigrationOptions>
    {
        ConcurrentQueue<IMigrationOptions> Migrations { get; }
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, IMigrationOptions migrationOptions);
    }
}
