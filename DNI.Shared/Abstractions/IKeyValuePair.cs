using System.Collections.Generic;

namespace DNI.Shared.Abstractions
{
    public interface IKeyValuePair<TKey, TValue>
    {
        KeyValuePair<TKey, TValue> KeyValuePair { get; }
        TKey Key { get;  }
        TValue Value { get; }
    }
}
