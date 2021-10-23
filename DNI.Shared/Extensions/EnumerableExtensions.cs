using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Shared.Extensions
{
    public static class EnumerableExtensions
    {
        public static void CopyTo<T>(this IEnumerable<T> items, ref IEnumerable<T> destination)
        {
            var destinationList = new List<T>(destination);

            foreach(var item in items)
            {
                if (!destination.Contains(item))
                {
                    destinationList.Add(item);
                }
            }

            destination = destinationList;
        }
    }
}
