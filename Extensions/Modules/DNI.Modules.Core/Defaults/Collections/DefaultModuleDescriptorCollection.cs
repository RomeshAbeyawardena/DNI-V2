using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Collections;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Modules.Core.Defaults.Collections
{
    public class DefaultModuleDescriptorCollection : CollectionBase<IModuleDescriptor>, IModuleDescriptorCollection
    {
        public DefaultModuleDescriptorCollection(IEnumerable<IModuleDescriptor> moduleDescriptors)
            : base(moduleDescriptors)
        {

        }

        public IEnumerable<Type> Types => this.Select(m => m.Type);
    }
}
