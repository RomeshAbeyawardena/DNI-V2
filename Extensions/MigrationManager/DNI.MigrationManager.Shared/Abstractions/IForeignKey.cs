using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IForeignKey
    {
        ITableConfiguration TableConfiguration { get; }
        ITableConfiguration ForeignTableConfiguration { get; }
        string ColumnName { get; }
    }
}
