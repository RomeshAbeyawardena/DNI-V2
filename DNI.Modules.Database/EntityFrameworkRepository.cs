using DNI.Shared.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Database
{
    public class EntityFrameworkRepository<TDbContext, T> : IRepository<T>, IAsyncRepository<T>
        where TDbContext : DbContext
        where T : class, IIdentifier
    {
        private readonly TDbContext context;

        public EntityFrameworkRepository(TDbContext context)
        {
            this.context = context;
            this.DbSet = context.Set<T>();
        }

        public DbSet<T> DbSet { get; }

        public IQueryable<T> Query => DbSet;

        public IQueryable<T> ReadonlyQuery => Query.AsNoTracking();

        public T Get(IIdentifier identifier)
        {
            return DbSet.Find(identifier.Id);
        }

        public ValueTask<T> GetAsync(IIdentifier identifier, CancellationToken cancellationToken)
        {
            return DbSet.FindAsync(new[] { identifier.Id }, cancellationToken);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
