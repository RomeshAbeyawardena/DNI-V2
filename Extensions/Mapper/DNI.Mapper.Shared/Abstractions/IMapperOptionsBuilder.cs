using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mapper.Shared.Abstractions
{
    public interface IMapperOptionsBuilder
    {
        IMapperOptionsBuilder AddAssembly(Assembly assembly);
        IMapperOptionsBuilder AddAssembly<T>();
        IMapperOptionsBuilder AddModuleAssemblies();
        IMapperOptions Build();
    }
}
