using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IAssemblyOptions
    {
        bool Discoverable { get; set; }
        bool Injectable { get; set; }
        bool OnStartup { get; set; }
    }
}
