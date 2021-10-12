using System;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleResult
    {
        bool IsException { get; }
        bool Haltable { get; }
        object Result { get; }
        Exception Exception { get; }
    }
}
