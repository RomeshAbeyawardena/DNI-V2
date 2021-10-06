using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Core.Defaults
{
    public class DefaultMediatorModuleOptionsBuilder : AssemblyOptionsBuilderBase, IMediatorModuleOptionsBuilder
    {
        private bool useModuleAssemblies;
        public IMediatorModuleOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        IMediatorModuleOptionsBuilder IMediatorModuleOptionsBuilder.AddAssembly(Assembly assembly)
        {
            base.AddAssembly(assembly);
            return this;
        }

        IMediatorModuleOptionsBuilder IMediatorModuleOptionsBuilder.AddAssembly(Type type)
        {
            base.AddAssembly(type);
            return this;
        }

        IMediatorModuleOptionsBuilder IMediatorModuleOptionsBuilder.AddAssembly<T>()
        {
            base.AddAssembly<T>();
            return this;
        }

        IMediatorModuleOptions IMediatorModuleOptionsBuilder.Build()
        {
            return new DefaultMediatorModuleOptions(base.Build(), useModuleAssemblies);
        }
    }
}
