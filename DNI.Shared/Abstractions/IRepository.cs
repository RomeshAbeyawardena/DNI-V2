using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IRepository<T>
        where T : class, IIdentifier
    {
        IQueryable<T> Query { get; }
        IQueryable<T> ReadonlyQuery { get; }
        T Get(IIdentifier identifier);
        int SaveChanges();
    }

    public interface IAsyncRepository<T>
        where T : class, IIdentifier
    {
        ValueTask<T> GetAsync(IIdentifier identifier, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
