using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
