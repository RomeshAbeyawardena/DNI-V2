using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class CollectionBase<T> : IEnumerable<T>
    {
        private readonly ConcurrentBag<T> items;

        public CollectionBase()
        {
            items = new ConcurrentBag<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        protected ConcurrentBag<T> Items => items;
    }
}
