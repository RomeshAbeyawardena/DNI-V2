using System;
using System.Collections;
using System.Collections.Generic;

namespace DNI.Shared.Base
{
    public abstract class CollectionBase<T> : Abstractions.ICollection<T>
    {
        private readonly List<T> collection;

        public CollectionBase()
            : this(false)
        {

        }

        public CollectionBase(IEnumerable<T> items, bool isReadonly = false)

        {
            collection = new List<T>(items);
            IsReadOnly = isReadonly;
        }

        public CollectionBase(bool isReadonly)
            : this(Array.Empty<T>(), isReadonly)
        {

        }

        public T this[int index] { get => collection[index]; set => collection[index] = value; }

        public int Count => collection.Count;

        public bool IsReadOnly { get; }

        public void Add(T item)
        {
            collection.Add(item);
        }

        public void AddRange(IEnumerable<T> items)
        {
            collection.AddRange(items);
        }

        public void Clear()
        {
            collection.Clear();
        }

        public bool Contains(T item)
        {
            return collection.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return collection.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            collection.Insert(index, item);
        }

        public bool Remove(T item)
        {
            return collection.Remove(item);
        }

        public void RemoveAt(int index)
        {
            collection.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
