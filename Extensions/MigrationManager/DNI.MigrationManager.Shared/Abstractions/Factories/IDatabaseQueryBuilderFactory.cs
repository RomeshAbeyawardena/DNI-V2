using DNI.MigrationManager.Shared.Abstractions.Builders;

namespace DNI.MigrationManager.Shared.Abstractions.Factories
{
    public interface IDatabaseQueryBuilderFactory
    {
        IDatabaseQueryBuilder GetDatabaseQueryBuilder(string name);
    }
}
