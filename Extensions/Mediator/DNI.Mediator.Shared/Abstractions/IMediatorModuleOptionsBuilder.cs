using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IMediatorModuleOptionsBuilder : IAssemblyOptionsBuilder
    {
        new IMediatorModuleOptionsBuilder AddAssembly(Assembly assembly);
        new IMediatorModuleOptionsBuilder AddAssembly(Type type);
        new IMediatorModuleOptionsBuilder AddAssembly<T>();
        IMediatorModuleOptionsBuilder AddModuleAssemblies();
        new IMediatorModuleOptions Build();
    }
}
