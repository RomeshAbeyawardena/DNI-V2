using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultAssemblyOptions : IAssemblyOptions
    {
        public bool Discoverable { get; set; }
        public bool Injectable { get; set; }
        public bool OnStartup { get; set; }
    }
}
