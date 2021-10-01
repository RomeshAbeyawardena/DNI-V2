using DNI.MigrationManager.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Defaults
{
    public class DefaultTableConfiguration : ITableConfiguration
    {
        public string PrimaryKey { get; set; }
        public string TableName { get; }
        public string Schema { get; }
        public IEnumerable<IDataColumn> DataColumns { get; }

        public DefaultTableConfiguration(string tableName, string schema, IEnumerable<IDataColumn> dataColumns)
        {
            TableName = tableName;
            Schema = schema;
            DataColumns = dataColumns;
        }

    }
}
