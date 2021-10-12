using System.Collections.Generic;
using System.Linq;

namespace DNI.Shared.Extensions
{
    public static class DictionaryExtensions
    {
        public static IEnumerable<KeyValuePair<TKey, TValue>> ToArray<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return dictionary.Select(a => KeyValuePair.Create(a.Key, a.Value));
        }
    }
}
