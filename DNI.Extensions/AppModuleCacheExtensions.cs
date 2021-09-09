namespace DNI.Extensions 
{
    using System;
    using DNI.Shared.Abstractions;

    public static class AppModuleCacheExtensions 
    {
        public static void RegisterModule<TAppModule>(this IAppModuleCache appModuleCache)
            where TAppModule : class, IAppModule
        {
            appModuleCache.RegisterModule(typeof(TAppModule));
        }
    }
}