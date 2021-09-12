using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core
{
    public class AppModuleCache<TAppModule> : AppModuleCacheBase, IAppModuleCache<TAppModule>
        where TAppModule : class, IAppModule
    {
        public AppModuleCache(IServiceProvider serviceProvider)
            : base(serviceProvider)
        {

        }

        public IAppModuleCache<TAppModule> RegisterAssembly<T>()
        {
            RegisterAssembly(typeof(T).Assembly);
            return this;
        }

        IAppModuleCache<TAppModule> IAppModuleCache<TAppModule>.RegisterModule<TRequiredAppModule>(IConfig config, Action<IAppModuleCache<TRequiredAppModule>> appModuleCacheBuilder)
        {
            RegisterModule(typeof(TRequiredAppModule), config, v => appModuleCacheBuilder(v as IAppModuleCache<TRequiredAppModule>));
            return this;
        }
    }
}
