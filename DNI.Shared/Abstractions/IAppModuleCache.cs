namespace DNI.Shared.Abstractions
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    public interface IAppModuleCache : IEnumerable<Type>
    {
        void RegisterAssembly(Assembly assembly);
        IEnumerable<Type> RegisteredTypes { get; }
        IEnumerable<Assembly> RegisteredAssemblies { get; }
        void RegisterModule(Type appModuleType, IConfig config = null, Action<IAppModuleCache> appModuleCache = null);
    }

    public interface IAppModuleCache<TAppModule> : IAppModuleCache
        where TAppModule : class, IAppModule
    {
        IAppModuleCache<TAppModule> RegisterAssembly<T>();
        IAppModuleCache<TAppModule> RegisterModule<TRequiredAppModule>(IConfig config = null, Action<IAppModuleCache<TRequiredAppModule>> appModuleCache = null)
            where TRequiredAppModule : class, IAppModule;
    }
}