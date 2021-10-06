using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IMediatorModuleOptions : IEnumerable<Assembly>
    {
        
    }
}
