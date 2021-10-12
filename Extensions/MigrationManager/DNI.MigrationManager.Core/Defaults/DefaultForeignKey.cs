using DNI.MigrationManager.Shared.Abstractions;

namespace DNI.MigrationManager.Core.Defaults
{
    public class DefaultForeignKey : IForeignKey
    {
        public DefaultForeignKey(ITableConfiguration tableConfiguration, ITableConfiguration foreignTableConfiguration, string columnName)
        {
            TableConfiguration = tableConfiguration;
            ForeignTableConfiguration = foreignTableConfiguration;
            ColumnName = columnName;
        }

        public string ColumnName { get; }
        public ITableConfiguration TableConfiguration { get; }
        public ITableConfiguration ForeignTableConfiguration { get; }
    }
}
