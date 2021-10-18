using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptions
    {
        bool UseModuleAssemblies { get; }
        IEnumerable<Assembly> Assemblies { get; }
    }
}
