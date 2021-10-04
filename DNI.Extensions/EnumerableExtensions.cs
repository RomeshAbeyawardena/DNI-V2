using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Extensions
{
    public static class EnumerableExtensions
    {
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            items.ForEach(item => list.Add(item));
        }

        public static IEnumerable<T> AppendAll<T>(this IEnumerable<T> items, IEnumerable<T> itemsToAppend)
        {
            var itemList = new List<T>(items);
            itemList.AddRange(itemsToAppend);

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
    }
}
