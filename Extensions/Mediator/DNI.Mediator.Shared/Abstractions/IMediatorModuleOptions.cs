using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IMediatorModuleOptions : IEnumerable<Assembly>
    {
        IEnumerable<Type> HandledExceptionTypes { get; }
        bool UseModuleAssemblies { get; }
    }
}
