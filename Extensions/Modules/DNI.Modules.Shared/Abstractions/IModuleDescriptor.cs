using DNI.Shared.Abstractions;
using System;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleDescriptor
    {
        string Usage { get; }
        Guid Id { get; }
        Type Type { get; }
        IKeyValuePair<Guid, Type> ToKeyValuePair();
    }
}
