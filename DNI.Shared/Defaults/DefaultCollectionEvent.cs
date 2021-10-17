using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;

namespace DNI.Shared.Defaults
{
    public class DefaultCollectionEvent<T> : ICollectionEvent<T>
    {
        public T Item { get; set; }

        public EventType EventType { get; set; }
    }
}
