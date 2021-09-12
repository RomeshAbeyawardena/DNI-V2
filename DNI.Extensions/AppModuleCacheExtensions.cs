namespace DNI.Extensions 
{
    using System;
    using DNI.Shared.Abstractions;

    public static class AppModuleCacheExtensions 
    {
        public static void RegisterModule<TAppModule, TAppModuleLoader>(this IGlobalAppModuleCache<TAppModuleLoader> appModuleCache)
            where TAppModule : class, IAppModule
            where TAppModuleLoader: class, IAppModuleLoader
        {
            appModuleCache.RegisterModule(typeof(TAppModule));
        }

        public static IAppModuleCache RegisterModule<TAppModule>(this IAppModuleCache appModuleCache, Action<IAppModuleCache<TAppModule>> appModuleCacheBuilder = null)
            where TAppModule : class, IAppModule
        {
            appModuleCache.RegisterModule(typeof(TAppModule), appModuleCache: s => appModuleCacheBuilder?.Invoke(s as IAppModuleCache<TAppModule>));
            return appModuleCache;
        }
    }
}