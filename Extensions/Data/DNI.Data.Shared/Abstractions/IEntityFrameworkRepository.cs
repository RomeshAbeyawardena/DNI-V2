using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace DNI.Data.Shared.Abstractions
{
    public interface IEntityFrameworkRepository<TDbContext, T> : IRepository<T>
        where TDbContext : DbContext
        where T : class
    {
        IObserver<EntityEntry<T>> EntityEntryState { get; }
    }
}
