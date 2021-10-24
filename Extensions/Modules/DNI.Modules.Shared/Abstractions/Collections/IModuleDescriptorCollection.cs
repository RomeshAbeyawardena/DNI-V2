using System;
using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions.Collections
{
    public interface IModuleDescriptorCollection : IList<IModuleDescriptor>, IEnumerable<IModuleDescriptor>
    {
        IEnumerable<Type> Types { get; }
    }
}
