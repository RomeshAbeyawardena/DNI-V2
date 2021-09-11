namespace DNI.ModuleLoader.Core 
{
    using System;
    using DNI.Shared.Abstractions;
    using System.Collections.Generic;
    using System.Collections;
    using DNI.ModuleLoader.Core.Base;

    public class GlobalModuleCache<TAppModuleLoader> : AppModuleCacheBase, IGlobalAppModuleCache<TAppModuleLoader>
        where TAppModuleLoader : class, IAppModuleLoader
    {
        public IGlobalAppModuleCache<TAppModuleLoader> RegisterModule<TAppModule>()
            where TAppModule : IAppModule
        {
            RegisterModule(typeof(TAppModule));
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}