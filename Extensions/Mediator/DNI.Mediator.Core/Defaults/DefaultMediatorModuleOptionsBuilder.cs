using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mediator.Core.Defaults
{
    public class DefaultMediatorModuleOptionsBuilder : AssemblyOptionsBuilderBase, IMediatorModuleOptionsBuilder
    {
        private bool useModuleAssemblies;
        private IEnumerable<Type> handledExceptionTypes;
        public IMediatorModuleOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        public IMediatorModuleOptionsBuilder SetHandledExceptionTypes(IEnumerable<Type> handledExceptionTypes)
        {
            this.handledExceptionTypes = handledExceptionTypes;
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
            return new DefaultMediatorModuleOptions(base.Build(), useModuleAssemblies) { HandledExceptionTypes = handledExceptionTypes };
        }
    }
}
