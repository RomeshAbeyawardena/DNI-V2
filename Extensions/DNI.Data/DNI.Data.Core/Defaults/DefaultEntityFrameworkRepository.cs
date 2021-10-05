using DNI.Data.Shared.Abstractions;
using DNI.Data.Shared.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Core.Defaults
{
    public class DefaultEntityFrameworkRepository<TDbContext, T> : BaseRepository<T>, IEntityFrameworkRepository<TDbContext, T>
        where T: class
        where TDbContext : DbContext
    {
        private readonly TDbContext dbContext;
        private readonly DbSet<T> dbSet;
        private readonly ISubject<EntityEntry<T>> stateSubject;
        public DefaultEntityFrameworkRepository(TDbContext dbContext, ISubject<EntityEntry<T>> subject)
        {
            stateSubject = subject;
            this.dbContext = dbContext;
            dbSet = dbContext.Set<T>();
        }

        public IObserver<EntityEntry<T>> EntityEntryState => stateSubject;

        public override void Add(T item)
        {
            stateSubject.OnNext(dbSet.Add(item));
        }

        public override ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public override T Find(params object[] parameters)
        {
            return dbSet.Find(parameters);
        }

        public override Task<T> FindAsync(IEnumerable<T> parameters, CancellationToken cancellationToken)
        {
            return dbSet.FindAsync(parameters, cancellationToken).AsTask();
        }

        public override void OnDispose(bool disposing)
        {
           
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
            stateSubject.OnNext(dbContext.Update(item));
        }
    }
}
