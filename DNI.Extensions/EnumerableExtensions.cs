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
            foreach (var item in items)
            {
                action?.Invoke(item);
            }
        }

        public static IEnumerable<TResult> ForEach<T, TResult>(this IEnumerable<T> items, Func<T,TResult> action)
        {
            var resultList = new List<TResult>();
            foreach (var item in items)
            {
                resultList.Add(action(item));
            }

            return resultList;
        }
    }
}
