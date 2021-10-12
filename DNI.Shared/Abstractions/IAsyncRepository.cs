using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IAsyncRepository<T> : IRepository<T>, IAsyncDisposable
    {
        Task<T> FindAsync(IEnumerable<T> parameters, CancellationToken cancellationToken);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
