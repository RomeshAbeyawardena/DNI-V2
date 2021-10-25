using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Shared.Base
{
    public abstract class AssemblyOptionsBuilderBase : CollectionBase<Assembly>, IAssemblyOptionsBuilder
    {
        public IAssemblyOptionsBuilder AddAssembly<T>()
        {
            return AddAssembly(typeof(T));
        }

        public IAssemblyOptionsBuilder AddAssembly(Type type)
        {
            return AddAssembly(type.Assembly);
        }

        public IAssemblyOptionsBuilder AddAssembly(Assembly assembly)
        {
            Add(assembly);
            return this;
        }

        public IEnumerable<Assembly> BuildAssemblies()
        {
            return this;
        }
    }
}
