using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared
{
    public class DefaultAppModuleLoaderOptions : IAppModulesLoaderOptions
    {
        public IEnumerable<string> ModuleHintPaths { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
