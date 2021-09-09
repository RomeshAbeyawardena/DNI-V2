namespace DNI.Extensions 
{
    using System;
    using DNI.Shared.Abstractions;

    public static class AppModuleCacheExtensions 
    {
        public static void RegisterModule<TAppModule, TAppModuleLoader>(this IAppModuleCache<TAppModuleLoader> appModuleCache)
            where TAppModule : class, IAppModule
            where TAppModuleLoader: class, IAppModuleLoader
        {
            appModuleCache.RegisterModule(typeof(TAppModule));
        }
    }
}