using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
