using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IAppModulesLoaderOptions
    {
        IEnumerable<string> ModuleHintPaths { get; set; }
        bool ContinueOnError { get; set; }
    }
}
