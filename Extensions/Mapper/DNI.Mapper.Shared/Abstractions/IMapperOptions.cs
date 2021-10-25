using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptions : DNI.Shared.Abstractions.ICollection<Assembly>
    {
        bool UseModuleAssemblies { get; }
    }
}
