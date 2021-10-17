using DNI.Shared.Abstractions;
using DNI.Shared.Defaults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public class ObservableCollectionBase<T> : IObservableCollection<T>
    {
        private readonly List<T> collectionList;
        private readonly ISubject<ICollectionEvent<T>> subject;

        public ObservableCollectionBase()
        {
            collectionList = new List<T>();
        }

        public ObservableCollectionBase(IEnumerable<T> items, bool isReadonly)
        {
            collectionList = new List<T>(items);
            IsReadOnly = isReadonly;
        }

        public T this[int index] { get => collectionList[index]; set => collectionList[index] = value; }

        public IObserver<ICollectionEvent<T>> OnChange => subject;

        public int Count => collectionList.Count;

        public bool IsReadOnly { get; }

        public void Add(T item)
        {
            subject.OnNext(new DefaultCollectionEvent<T> { Item = item, EventType = Enumerations.EventType.Add });
            collectionList.Add(item);
        }

        public void Clear()
        {
            collectionList.Clear();
        }

        public bool Contains(T item)
        {
            return collectionList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            collectionList.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return collectionList.GetEnumerator();
        }

        public int IndexOf(T item)
        {
            return collectionList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            subject.OnNext(new DefaultCollectionEvent<T> { Item = item, EventType = Enumerations.EventType.Add });
            collectionList.Insert(index, item);
        }

        public bool Remove(T item)
        {
            subject.OnNext(new DefaultCollectionEvent<T> { Item = item, EventType = Enumerations.EventType.Add });
            return collectionList.Remove(item);
        }

        public void RemoveAt(int index)
        {
            collectionList.RemoveAt(index);
            subject.OnNext(new DefaultCollectionEvent<T> { Item = collectionList[index], EventType = Enumerations.EventType.Add });
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collectionList.GetEnumerator();
        }
    }
}
