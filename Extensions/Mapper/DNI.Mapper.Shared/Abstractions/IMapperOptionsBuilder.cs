using DNI.Modules.Shared.Builders;
using System;
using System.Reflection;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptionsBuilder : IModuleOptionsAssemblyBuilder<IMapperOptions>
    {
        new IMapperOptionsBuilder AddAssembly(Assembly assembly);
        new IMapperOptionsBuilder AddAssembly(Type type);
        new IMapperOptionsBuilder AddAssembly<T>();
        IMapperOptionsBuilder AddModuleAssemblies();
    }
}
