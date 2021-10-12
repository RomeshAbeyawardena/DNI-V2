using System;
using System.Collections.Generic;

namespace DNI.Shared.Abstractions
{
    public interface IEventDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        event EventHandler<DictionaryEventArgs<TValue>> EventOccurred;
    }
}
