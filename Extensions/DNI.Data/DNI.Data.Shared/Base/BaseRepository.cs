using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Shared.Base
{
    public abstract class BaseRepository<T> : DisposableBase, IRepository<T>, IAsyncRepository<T>
    {
        public IQueryable<T> Query { get; protected set; }

        public abstract void Add(T item);
        public abstract ValueTask DisposeAsync();
        public abstract void OnDispose(bool disposing);
        public abstract T Find(params object[] parameters);
        public abstract Task<T> FindAsync(IEnumerable<T> parameters, CancellationToken cancellationToken);
        public abstract int SaveChanges();
        public abstract Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        public abstract void Update(T item);

        public override void Dispose(bool disposing)
        {
            OnDispose(disposing);
        }
    }
}
