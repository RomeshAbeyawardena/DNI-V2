using System;
using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions
{
    public interface ISubscriptionManager<T> : IDisposable
    {
        IEnumerable<IDisposable> Subscribers { get; }
        void Subscribe(Action<T> onNext);
        void Subscribe(Action<T> onNext, Action onComplete);
    }
}
