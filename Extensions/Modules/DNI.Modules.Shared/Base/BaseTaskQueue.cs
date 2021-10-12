using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Base
{
    public abstract class BaseTaskQueue : ITaskQueue
    {
        protected IProducerConsumerCollection<Func<CancellationToken, Task>> Queue { get; }

        protected BaseTaskQueue(IProducerConsumerCollection<Func<CancellationToken, Task>> queue)
        {
            this.Queue = queue;
        }

        public int Count => Queue.Count;

        public bool IsSynchronized => Queue.IsSynchronized;

        public object SyncRoot => Queue.SyncRoot;

        void IProducerConsumerCollection<Func<CancellationToken, Task>>.CopyTo(Func<CancellationToken, Task>[] array, int index)
        {
            Queue.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            Queue.CopyTo(array, index);
        }

        public abstract bool Dequeue(out Func<CancellationToken, Task> task);

        Func<CancellationToken, Task>[] IProducerConsumerCollection<Func<CancellationToken, Task>>.ToArray()
        {
            return Queue.ToArray();
        }

        bool IProducerConsumerCollection<Func<CancellationToken, Task>>.TryAdd(Func<CancellationToken, Task> item)
        {
            return Queue.TryAdd(item);
        }

        bool IProducerConsumerCollection<Func<CancellationToken, Task>>.TryTake(out Func<CancellationToken, Task> item)
        {
            return Queue.TryTake(out item);
        }

        public IEnumerator<Func<CancellationToken, Task>> GetEnumerator()
        {
            return Queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
