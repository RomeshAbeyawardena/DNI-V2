using DNI.Modules.Core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleAssemblyResolverConfigurationOptions
    {
        public Dictionary<string, AssemblyOptions> Modules { get; set; }
    }
}
