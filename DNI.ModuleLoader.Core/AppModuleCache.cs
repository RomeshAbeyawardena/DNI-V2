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
        IAppModuleCache<TAppModule> IAppModuleCache<TAppModule>.RegisterModule<TRequiredAppModule>(IConfig config)
        {
            RegisterModule(typeof(TRequiredAppModule));
            return this;
        }
    }
}
