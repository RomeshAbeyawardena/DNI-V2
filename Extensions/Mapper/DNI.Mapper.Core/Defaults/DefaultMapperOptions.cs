using DNI.Mapper.Shared.Abstractions;
using DNI.Shared.Base;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Core.Defaults
{
    public class DefaultMapperOptions : CollectionBase<Assembly>, IMapperOptions
    {
        public bool UseModuleAssemblies { get; set; }
    }
}
