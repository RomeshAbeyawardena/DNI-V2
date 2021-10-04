using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Shared
{
    public interface IEntityFrameworkRepository<TDbContext, T> : IRepository<T>
        where TDbContext : DbContext
        where T : class
    {
        IObserver<EntityEntry<T>> EntityEntryState { get; }
    }
}
