using DNI.Mapper.Shared.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Core.Defaults
{
    public class DefaultMapperOptions : IMapperOptions
    {
        public bool UseModuleAssemblies { get; set; }

        public IEnumerable<Assembly> Assemblies { get; set; }
    }
}
