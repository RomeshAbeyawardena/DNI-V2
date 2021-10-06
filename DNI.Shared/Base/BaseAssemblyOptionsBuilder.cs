using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class BaseAssemblyOptionsBuilder : CollectionBase<Assembly>, IAssemblyOptionsBuilder
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
    }
}
