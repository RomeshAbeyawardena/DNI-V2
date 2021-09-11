namespace DNI.Shared.Abstractions 
{
    using System;
    using System.Collections.Generic;

    public interface IAppModuleCache : IEnumerable<Type>
    {
        IEnumerable<Type> RegisteredTypes { get; }
        void RegisterModule(Type appModuleType);
    }

    public interface IAppModuleCache<TAppModule> : IAppModuleCache
        where TAppModule : class, IAppModule
    {
        IAppModuleCache<TAppModule> RegisterModule<TRequiredAppModule>()
            where TRequiredAppModule : class, IAppModule;
    }

    public interface IGlobalAppModuleCache<TAppModuleLoader> : IAppModuleCache
        where TAppModuleLoader : class, IAppModuleLoader
    {
        /// <summary>
        /// Registers an <see cref="IAppModule">app module</see> that contains a list of services and infrastructure that is required by another <see cref="IAppModule"/> or globally to be used by all <see cref="IAppModule">modules</see>
        /// </summary>
        /// <typeparam name="TAppModule"></typeparam>
        /// <returns></returns>
        IGlobalAppModuleCache<TAppModuleLoader> RegisterModule<TAppModule>()
            where TAppModule : IAppModule;
    }
}