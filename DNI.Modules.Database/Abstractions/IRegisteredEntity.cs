using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Database.Abstractions
{
    public interface IRegisteredEntity
    {
        Type Type { get; }
    }

    public interface IRegisteredEntity<TDbContext> : IRegisteredEntity
        where TDbContext : DbContext
    {

    }
}
