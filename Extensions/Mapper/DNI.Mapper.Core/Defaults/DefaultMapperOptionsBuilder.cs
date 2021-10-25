using DNI.Mapper.Shared.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Core.Defaults
{
    public class DefaultMapperOptionsBuilder : IMapperOptionsBuilder
    {
        private readonly List<Assembly> assemblies;
        
        private bool useModuleAssemblies = false;
        
        public DefaultMapperOptionsBuilder()
        {
            assemblies = new();
        }

        public IMapperOptionsBuilder AddAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
            return this;
        }

        public IMapperOptionsBuilder AddAssembly<T>()
        {
            return AddAssembly(typeof(T).Assembly);
        }

        public IMapperOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        public IMapperOptions Build()
        {
            var opts = new DefaultMapperOptions { UseModuleAssemblies = useModuleAssemblies };
            opts.AddRange(assemblies);
            return opts;
        }
    }
}
