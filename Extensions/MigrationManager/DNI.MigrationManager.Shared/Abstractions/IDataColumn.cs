using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IDataColumn
    {
        string Name { get; }
        Type Type { get; }
        int? Length { get; }
        string TypeName { get; }
        object DefaultValue { get; }
        bool IsRequired { get; }
        IEnumerable<IForeignKey> ForeignKeys { get; }
    }
}
