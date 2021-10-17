using System;
using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions
{
    public interface ICompiledModuleConfiguration
    {
        IDictionary<Type, object> Options { get; }
        IEnumerable<IModule> Modules { get; }
    }
}
