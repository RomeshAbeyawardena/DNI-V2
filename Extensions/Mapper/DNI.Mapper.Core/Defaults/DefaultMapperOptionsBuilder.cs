using DNI.Mapper.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            return new DefaultMapperOptions { Assemblies = assemblies, UseModuleAssemblies = useModuleAssemblies };
        }
    }
}
