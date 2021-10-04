using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
