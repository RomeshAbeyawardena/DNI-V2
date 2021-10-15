using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultCompiledModuleConfiguration : ICompiledModuleConfiguration
    {
        public IEnumerable<IModule> Modules { get; set; }
    }
}
