using DNI.Modules.Shared.Base.Buillders;
using DNI.Modules.Shared.Builders;
using DNI.Shared.Abstractions;
using DNI.Shared.Defaults.Collections;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Modules.Shared.Base.Builders
{
    public abstract class ModuleOptionsAssemblyBuilderBase<TOptions> : ModuleOptionsBuilderBase<TOptions>, IModuleOptionsAssemblyBuilder<TOptions>
    {
        private readonly IAssemblyCollection assemblyCollection;

        public abstract TOptions BuildOptions(IEnumerable<Assembly> builtAssemblies);

        IAssemblyOptionsBuilder IAssemblyOptionsBuilder.AddAssembly<T>()
        {
            AddAssembly<T>();
            return this;
        }

        IAssemblyOptionsBuilder IAssemblyOptionsBuilder.AddAssembly(Type type)
        {
            AddAssembly(type.Assembly);
            return this;
        }

        IAssemblyOptionsBuilder IAssemblyOptionsBuilder.AddAssembly(Assembly assembly)
        {
            AddAssembly(assembly);
            return this;
        }

        public IEnumerator<Assembly> GetEnumerator()
        {
            return assemblyCollection.GetEnumerator();
        }

        IEnumerable<Assembly> IAssemblyOptionsBuilder.BuildAssemblies()
        {
            return assemblyCollection.ToArray();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return assemblyCollection.GetEnumerator();
        }


        public ModuleOptionsAssemblyBuilderBase()
        {
            assemblyCollection = new DefaultAssemblyCollection();
        }

        public override TOptions Build()
        {


            return BuildOptions(assemblyCollection.ToArray());
        }

        public IModuleOptionsAssemblyBuilder<TOptions> AddAssembly<T>()
        {
            return AddAssembly(typeof(T));
        }

        public IModuleOptionsAssemblyBuilder<TOptions> AddAssembly(Type type)
        {
            return AddAssembly(type.Assembly);
        }

        public IModuleOptionsAssemblyBuilder<TOptions> AddAssembly(Assembly assembly)
        {
            assemblyCollection.Add(assembly);
            return this;
        }
    }
}
