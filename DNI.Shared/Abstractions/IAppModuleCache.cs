namespace DNI.Shared.Abstractions 
{
    using System;
    using System.Collections.Generic;

    public interface IAppModuleCache : IEnumerable<Type>
    {
        IEnumerable<Type> RegisteredTypes { get; }
        void RegisterModule(Type appModuleType);
    }

    public interface IAppModuleCache<TAppModuleLoader> : IAppModuleCache
        where TAppModuleLoader : class, IAppModuleLoader
    {
        void RegisterModule<TAppModule>();
    }
}