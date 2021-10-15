using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface ISubscriptionManager<T> : IDisposable
    {
        IEnumerable<IDisposable> Subscribers { get; }
        void Subscribe(Action<T> onNext);
        void Subscribe(Action<T> onNext, Action onComplete);
    }
}
