using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.ForEach(item => list.Add(item));
        }

        public static IEnumerable<T> RemoveAt<T>(this IEnumerable<T> items, int index)
        {
            var itemList = new List<T>(items);

            itemList.RemoveAt(index);

            return itemList;
        }

        public static IEnumerable<T> AppendMany<T>(this IEnumerable<T> items, IEnumerable<T> itemsToAppend)
        {
            var itemList = new List<T>(items);

            if (itemsToAppend == null)
                return itemList;

            itemList.AddRange(itemsToAppend.ToArray());

            return itemList;
        }

        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            if (items == null)
            {
                return;
            }

            foreach (var item in items)
            {
                action?.Invoke(item);
            }
        }

        public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> items, Func<T, TResult> action)
        {
            var itemList = new List<TResult>();
            if (items == null)
            {
                return Array.Empty<TResult>();
            }

            foreach (var item in items)
            {
                if (action != null)
                {
                    itemList.Add(action.Invoke(item));
                }
            }

            return itemList;
        }
    }
}
