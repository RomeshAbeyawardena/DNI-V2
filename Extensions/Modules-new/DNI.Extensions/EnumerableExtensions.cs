using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class EnumerableExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach(var item in items)
            {
                action?.Invoke(item);
            }
        }

        public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> items, Func<T, TResult> action)
        {
            List<TResult> itemResultList = new();
            foreach(var item in items)
            {
                if (action != null)
                {
                    itemResultList.Add(action.Invoke(item));
                }
            }

            return itemResultList;
        }
        public static IEnumerable<T> ReplaceAt<T>(this IEnumerable<T> items, int index, T newItem)
        {
            var itemList = new List<T>(items);

            itemList[index] = newItem;

            return itemList;
        }
    }
}
