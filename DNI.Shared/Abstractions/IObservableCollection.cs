using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;

namespace DNI.Shared.Abstractions
{
    public interface ICollectionEvent<T>
    {
        T Item { get; }
        EventType EventType { get; }
    }

    public interface IObservableCollection<T> : IList<T>
    {
        IObserver<ICollectionEvent<T>> OnChange { get; }
    }
}
