using System.Reflection;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptions : DNI.Shared.Abstractions.ICollection<Assembly>
    {
        bool UseModuleAssemblies { get; }
    }
}
