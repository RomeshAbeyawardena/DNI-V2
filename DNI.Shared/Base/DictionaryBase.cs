using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class DictionaryBase<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, TValue> dictionary;

        public DictionaryBase()
        {
            dictionary = new ConcurrentDictionary<TKey, TValue>();
        }

        TValue IDictionary<TKey, TValue>.this[TKey key] { get => dictionary[key]; set => dictionary[key] = value; }

        ICollection<TKey> IDictionary<TKey, TValue>.Keys => dictionary.Keys;

        ICollection<TValue> IDictionary<TKey, TValue>.Values => dictionary.Values;

        int ICollection<KeyValuePair<TKey, TValue>>.Count => dictionary.Count;

        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => true;

        public void Add(TKey key, TValue value)
        {
            dictionary.TryAdd(key, value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            Add(item.Key, item.Value);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            dictionary.Clear();
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Contains(item);
        }

        bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            Array.Copy(dictionary.Select(k => k).ToArray(), array, dictionary.Count - arrayIndex);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return dictionary.GetEnumerator();
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            return dictionary.Remove(key, out var value);
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            return dictionary.Remove(item.Key, out var value);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return dictionary.TryGetValue(key, out value);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
