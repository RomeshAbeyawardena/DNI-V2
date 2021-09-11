namespace DNI.Shared.Abstractions
{
    using System;
    using System.Collections.Generic;

    public interface IAppModuleCache : IEnumerable<Type>
    {
        IEnumerable<Type> RegisteredTypes { get; }
        void RegisterModule(Type appModuleType, IConfig config = null);
    }

    public interface IAppModuleCache<TAppModule> : IAppModuleCache
        where TAppModule : class, IAppModule
    {
        IAppModuleCache<TAppModule> RegisterModule<TRequiredAppModule>(IConfig config = null)
            where TRequiredAppModule : class, IAppModule;
    }
}