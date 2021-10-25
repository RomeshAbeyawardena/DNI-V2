using DNI.Mapper.Shared.Abstractions;
using DNI.Modules.Shared.Base.Builders;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mapper.Core.Defaults
{
    public class DefaultMapperOptionsBuilder : ModuleOptionsAssemblyBuilderBase<IMapperOptions>, IMapperOptionsBuilder
    {
        private bool useModuleAssemblies = false;
        
        public new IMapperOptionsBuilder AddAssembly(Assembly assembly)
        {
            base.AddAssembly(assembly);
            return this;
        }

        public new IMapperOptionsBuilder AddAssembly<T>()
        {
            return AddAssembly(typeof(T).Assembly);
        }

        public IMapperOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        public override IMapperOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            var opts = new DefaultMapperOptions { UseModuleAssemblies = useModuleAssemblies };
            opts.AddRange(builtAssemblies);
            return opts;
        }
    }
}
