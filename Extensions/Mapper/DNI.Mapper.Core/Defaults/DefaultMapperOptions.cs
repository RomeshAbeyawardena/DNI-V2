using DNI.Mapper.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mapper.Core.Defaults
{
    public class DefaultMapperOptions : IMapperOptions
    {
        public bool UseModuleAssemblies { get; set; }

        public IEnumerable<Assembly> Assemblies { get; set; }
    }
}
