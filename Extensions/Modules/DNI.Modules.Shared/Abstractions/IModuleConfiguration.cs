using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        IServiceProvider ServiceProvider { get; set; }
        IDictionary<Type, object> Options { get; }
        IEnumerable<Type> ModuleTypes { get; set; }
        ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider, IEnumerable<IModule> configuredModules);
    }
}
