using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultTaskQueue : BaseTaskQueue
    {
        public DefaultTaskQueue()
            : base(new ConcurrentQueue<Func<CancellationToken, Task>>())
        {

        }

        public override bool Dequeue(out Func<CancellationToken, Task> task)
        {
            return base.Queue.TryTake(out task);
        }
    }

    public class DefaultTaskQueue<TResult> : DefaultTaskQueue, ITaskQueue<TResult>
    {
        public virtual bool Dequeue(out Func<CancellationToken, Task<TResult>> task)
        {
            task = null;
            var hasItems = base.Queue.TryTake(out var baseTask);

            if (hasItems)
            {
                task = (Func<CancellationToken, Task<TResult>>)baseTask;
            }

            return hasItems;
        }

        public bool TryAdd(Func<CancellationToken, Task<TResult>> item)
        {
            return Queue.TryAdd(item);
        }
    }
}
