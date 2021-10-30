using DNI.Data.Shared.Abstractions;
using DNI.Data.Shared.Base;
using DNI.Extensions;
using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using DNI.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Core.Defaults
{
    public class DefaultEntityFrameworkRepository<TDbContext, T> : BaseRepository<T>, IEntityFrameworkRepository<TDbContext, T>
        where T : class
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;
        private readonly DbSet<T> dbSet;
        private readonly ISubject<EntityEntry<T>> stateSubject;
        private readonly IClockProvider clockProvider;

        public DefaultEntityFrameworkRepository(TDbContext dbContext, ISubject<EntityEntry<T>> subject,
            IClockProvider clockProvider)
        {
            stateSubject = subject;
            this.clockProvider = clockProvider;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
            Query = dbSet;
        }

        public IObserver<EntityEntry<T>> EntityEntryState => stateSubject;

        public override void Add(T item)
        {
            clockProvider.UpdateValueMetaTags(item, MetaAction.Add);
            stateSubject.OnNext(dbSet.Add(item));
        }

        public override ValueTask DisposeAsync()
        {
            return (dbContext?.DisposeAsync()).GetValueOrDefault();
        }

        public override T Find(params object[] parameters)
        {
            return dbSet.Find(parameters);
        }

        public override Task<T> FindAsync(IEnumerable<object> parameters, CancellationToken cancellationToken)
        {
            return dbSet.FindAsync(parameters.ToArray(), cancellationToken: cancellationToken).AsTask();
        }

        public override void OnDispose(bool disposing)
        {
            dbContext?.Dispose();
        }

        public override int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return dbContext.SaveChangesAsync(cancellationToken);
        }

        public override void Update(T item)
        {
            var itemType = typeof(T);
            var keys = item.GetDictionary<KeyAttribute>();
            var existingItem = Find(keys.Select(a => a.Value).ToArray());

            if (existingItem != null)
            {
                //copy all
                item.Copy(existingItem);
            }

            clockProvider.UpdateValueMetaTags(existingItem, MetaAction.Update);
            stateSubject.OnNext(dbContext.Update(existingItem));
        }
    }
}
