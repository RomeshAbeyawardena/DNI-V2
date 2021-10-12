using DNI.Shared.Enumerations;

namespace DNI.Shared
{
    public class DictionaryEventArgs<TValue>
    {
        public EventType EventType { get; }

        public DictionaryEventArgs(EventType eventType, bool suceeded, TValue value)
        {
            EventType = eventType;
            Suceeded = suceeded;
            Value = value;
        }

        public bool Suceeded { get; }
        public TValue Value { get; }
    }
}
