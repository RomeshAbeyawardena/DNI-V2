using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        IEnumerable<Type> ModuleTypes { get; }
        ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider);
    }
}
