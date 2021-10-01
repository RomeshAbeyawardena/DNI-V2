using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Builders
{
    public interface IDictionaryBuilder<TKey, TValue> : IDictionary<TKey, TValue>
    {
        IDictionary<TKey, TValue> Dictionary { get; }
        new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value);
        IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair);
        new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair);
    }
}
