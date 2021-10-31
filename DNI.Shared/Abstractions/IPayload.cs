using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IPayload : IEnumerable<string>
    {
        string this[int index] { get; }
        string Name { get; }
        IEnumerable<string> Parameters { get; }
    }
}
