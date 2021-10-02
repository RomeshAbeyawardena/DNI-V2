using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IMigrationOptions
    {
        IEnumerable<Type> Types { get; }
        Func<IServiceProvider, IDbConnection> DbConnectionFactory { get; }
        IDictionary<Type, ITableConfiguration> TableConfiguration { get; }
    }
}
