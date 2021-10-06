using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
