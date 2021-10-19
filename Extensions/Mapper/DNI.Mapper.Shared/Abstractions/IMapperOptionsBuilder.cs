using System.Reflection;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptionsBuilder
    {
        IMapperOptionsBuilder AddAssembly(Assembly assembly);
        IMapperOptionsBuilder AddAssembly<T>();
        IMapperOptionsBuilder AddModuleAssemblies();
        IMapperOptions Build();
    }
}
