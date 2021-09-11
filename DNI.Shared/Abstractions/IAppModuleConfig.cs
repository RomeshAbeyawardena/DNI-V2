using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IAppModuleConfig<TAppModule> : IEnumerable<Type>
        where TAppModule: class, IAppModule
    {
        IAppModuleConfig<TAppModule> AddConfiguration(Type configType);
        IAppModuleConfig<TAppModule> AddConfiguration<TConfiguration>()
            where TConfiguration : class, IConfig;
    }
}
