using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions
{
    public interface ICompiledModuleConfiguration
    {
        IDictionary<IModuleDescriptor, object> Options { get; }
        IEnumerable<IModule> Modules { get; }
    }
}
