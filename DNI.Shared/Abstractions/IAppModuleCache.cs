namespace DNI.Shared.Abstractions 
{
    using System;
    using System.Collections.Generic;

    public interface IAppModuleCache<TAppModule>
        where TAppModule : class, IAppModule
    {
        IEnumerable<Type> RegisteredTypes { get; }
        void RegisterModule(Type appModuleType);
    }
}