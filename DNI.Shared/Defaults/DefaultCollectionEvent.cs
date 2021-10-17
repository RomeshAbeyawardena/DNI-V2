using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Defaults
{
    public class DefaultCollectionEvent<T> : ICollectionEvent<T>
    {
        public T Item { get; set; }

        public EventType EventType { get; set; }
    }
}
