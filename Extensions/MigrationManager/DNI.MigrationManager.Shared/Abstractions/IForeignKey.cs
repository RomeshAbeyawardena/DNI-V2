namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IForeignKey
    {
        ITableConfiguration TableConfiguration { get; }
        ITableConfiguration ForeignTableConfiguration { get; }
        string ColumnName { get; }
    }
}
