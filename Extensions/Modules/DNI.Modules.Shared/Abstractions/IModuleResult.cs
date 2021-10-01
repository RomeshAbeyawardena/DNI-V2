using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
