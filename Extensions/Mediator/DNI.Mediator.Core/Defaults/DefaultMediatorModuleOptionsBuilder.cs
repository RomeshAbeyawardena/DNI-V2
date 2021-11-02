using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Shared.Base.Builders;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mediator.Core.Defaults
{
    public class DefaultMediatorModuleOptionsBuilder : ModuleOptionsAssemblyBuilderBase<IMediatorModuleOptions>, IMediatorModuleOptionsBuilder
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
            AddAssembly(assembly);
            return this;
        }

        IMediatorModuleOptionsBuilder IMediatorModuleOptionsBuilder.AddAssembly(Type type)
        {
            AddAssembly(type);
            return this;
        }

        IMediatorModuleOptionsBuilder IMediatorModuleOptionsBuilder.AddAssembly<T>()
        {
            AddAssembly<T>();
            return this;
        }

        public override IMediatorModuleOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            return new DefaultMediatorModuleOptions(builtAssemblies, useModuleAssemblies)
            {
                HandledExceptionTypes = handledExceptionTypes
            };
        }
    }
}
