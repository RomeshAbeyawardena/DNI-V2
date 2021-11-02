using System.Collections.Generic;

namespace DNI.Shared.Abstractions
{
    public interface IPayload : IEnumerable<string>
    {
        string this[int index] { get; }
        string Name { get; }
        IEnumerable<string> Parameters { get; }
    }
}
