using System;
using System.Linq;

namespace DNI.Shared.Abstractions
{
    public interface IRepository<T> : IDisposable
    {
        T Find(params object[] parameters);
        void Add(T item);
        void Update(T item);
        IQueryable<T> Query { get; }
        int SaveChanges();
    }
}
