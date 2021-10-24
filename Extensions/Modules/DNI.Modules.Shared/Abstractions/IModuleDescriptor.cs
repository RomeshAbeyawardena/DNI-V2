using DNI.Shared.Abstractions;
using System;

namespace DNI.Modules.Shared.Abstractions
{
    /// <summary>
    /// Describes a module
    /// </summary>
    public interface IModuleDescriptor
    {
        bool Enabled { get; }
        string Usage { get; }
        Guid Id { get; }
        Type Type { get; }
        IKeyValuePair<Guid, Type> ToKeyValuePair();
    }
}
