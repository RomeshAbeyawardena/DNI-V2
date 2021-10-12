using DNI.Core.Defaults.Builders;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Extensions
{
    public static class DictionaryExtensions
    {
        public static IDictionary<TKey, TValue> Configure<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            var dictionaryBuilder = new DefaultDictionaryBuilder<TKey, TValue>(dictionary);
            return dictionary.Configure(buildAction);
        }

        public static IDictionaryBuilder<TKey, TValue> Configure<TKey, TValue>(this IDictionaryBuilder<TKey, TValue> dictionaryBuilder, Action<IDictionaryBuilder<TKey, TValue>> buildAction)
        {
            buildAction(dictionaryBuilder);

            return dictionaryBuilder;
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            items.ForEach(item => dictionary.Add(item));
        }

        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEnumerable<IKeyValuePair<TKey, TValue>> items)
        {
            AddRange(dictionary, items.Select(i => i.KeyValuePair));
        }
    }
}
