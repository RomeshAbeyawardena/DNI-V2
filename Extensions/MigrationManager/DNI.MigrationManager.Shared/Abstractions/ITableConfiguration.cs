using System.Collections.Generic;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface ITableConfiguration
    {
        string PrimaryKey { get; set; }
        string TableName { get; }
        string Schema { get; }
        IEnumerable<IDataColumn> DataColumns { get; }
    }
}
