using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleDescriptorCollection : IList<IModuleDescriptor>, IEnumerable<IModuleDescriptor>
    {
        IEnumerable<Type> Types { get; }
    }
}
