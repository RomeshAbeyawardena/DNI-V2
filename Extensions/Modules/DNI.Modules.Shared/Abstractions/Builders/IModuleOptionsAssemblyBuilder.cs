using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Builders
{
    public interface IModuleOptionsAssemblyBuilder<TOptions> : IModuleOptionsBuilder<TOptions>, IAssemblyOptionsBuilder
    {
        new IModuleOptionsAssemblyBuilder<TOptions> AddAssembly<T>();
        new IModuleOptionsAssemblyBuilder<TOptions> AddAssembly(Type type);
        new IModuleOptionsAssemblyBuilder<TOptions> AddAssembly(Assembly assembly);
    }
}
