using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IMediatorModuleOptionsBuilder : IAssemblyOptionsBuilder
    {
        IMediatorModuleOptionsBuilder SetHandledExceptionTypes(IEnumerable<Type> handledExceptions);
        new IMediatorModuleOptionsBuilder AddAssembly(Assembly assembly);
        new IMediatorModuleOptionsBuilder AddAssembly(Type type);
        new IMediatorModuleOptionsBuilder AddAssembly<T>();
        IMediatorModuleOptionsBuilder AddModuleAssemblies();
        new IMediatorModuleOptions Build();
    }
}
