using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Shared.Abstractions
{
    public interface IDbConnectionFactory : IDisposable
    {
        IDbConnection GetDbConnection(string connectionString);
    }
}
