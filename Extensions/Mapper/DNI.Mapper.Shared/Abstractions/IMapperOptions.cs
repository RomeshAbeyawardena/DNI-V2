using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptions
    {
        bool UseModuleAssemblies { get; }
        IEnumerable<Assembly> Assemblies { get; }
    }
}
