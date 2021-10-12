using System.Collections.Concurrent;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IMigrationManager : IProducerConsumerCollection<IMigrationOptions>
    {
        ConcurrentQueue<IMigrationOptions> Migrations { get; }
        IMigrationOptions GetMigrationOptions(string name);
        void Add(string name, IMigrationOptions migrationOptions);
    }
}
