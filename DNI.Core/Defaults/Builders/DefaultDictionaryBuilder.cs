using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Builders;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;

namespace DNI.Core.Defaults.Builders
{
    public static class DictionaryBuilder
    {
        public static IDictionaryBuilder<TKey, TValue> Create<TKey, TValue>()
        {
            return new DefaultDictionaryBuilder<TKey, TValue>();
        }

        public static IDictionary<TKey, TValue> Build<TKey, TValue>(Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            var dictionaryBuilder = Create<TKey, TValue>();
            buildAction?.Invoke(dictionaryBuilder);
            return dictionaryBuilder;
        }
    }

    /// <inheritdoc cref="IDictionaryBuilder{TKey, TValue} "/>
    public class DefaultDictionaryBuilder<TKey, TValue> : DictionaryBase<TKey, TValue>, IDictionaryBuilder<TKey, TValue>
    {
        public DefaultDictionaryBuilder()
            : this(new Dictionary<TKey, TValue>())
        {

        }

        public DefaultDictionaryBuilder(IDictionary<TKey, TValue> dictionary)
            : base(dictionary)
        {
            
        }

        public IDictionary<TKey, TValue> Dictionary => base.dictionary;

        public IDictionaryBuilder<TKey, TValue> Add(IKeyValuePair<TKey, TValue> keyValuePair)
        {
            return this.Add(keyValuePair.KeyValuePair);
        }

        public new IDictionaryBuilder<TKey, TValue> Add(TKey key, TValue value)
        {
            base.Add(key, value);
            return this;
        }

        public new IDictionaryBuilder<TKey, TValue> Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            return this.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
