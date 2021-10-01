using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core
{
    public static class DefaultKeyValuePair
    {
        public static IKeyValuePair<TKey, TValue> Create<TKey, TValue>(TKey key, TValue value)
        {
            return new DefaultKeyValuePair<TKey, TValue>(key, value);
        }
    }

    public class DefaultKeyValuePair<TKey, TValue> : IKeyValuePair<TKey, TValue>
    {
        public DefaultKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
        public KeyValuePair<TKey, TValue> KeyValuePair => System.Collections.Generic.KeyValuePair.Create(Key, Value);

        public TKey Key { get; }

        public TValue Value { get; }
    }
}
