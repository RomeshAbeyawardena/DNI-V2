using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Shared.Abstractions
{
    public interface IAssemblyOptionsBuilder : IEnumerable<Assembly>
    {
        IAssemblyOptionsBuilder AddAssembly<T>();
        IAssemblyOptionsBuilder AddAssembly(Type type);
        IAssemblyOptionsBuilder AddAssembly(Assembly assembly);
        IEnumerable<Assembly> Build();
    }
}
