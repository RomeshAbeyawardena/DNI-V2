using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Base
{
    public abstract class SubscriptionManagerBase<T> : ISubscriptionManager<T>
    {
        private readonly List<IDisposable> subscribersList;
        private readonly IObservable<T> observable;

        public IEnumerable<IDisposable> Subscribers => subscribersList;

        public abstract void Dispose(bool disposing);

        public SubscriptionManagerBase(IObservable<T> observable)
        {
            subscribersList = new List<IDisposable>();
            this.observable = observable;
        }

        public void Dispose()
        {
            Dispose(true);
            subscribersList.ForEach(d => d.Dispose());
            GC.SuppressFinalize(this);
        }

        public void Subscribe(Action<T> onNext)
        {
            subscribersList.Add(observable.Subscribe(onNext));
        }

        public void Subscribe(Action<T> onNext, Action onComplete)
        {
            subscribersList.Add(observable.Subscribe(onNext, onComplete));
        }
    }
}
