namespace DNI.ModuleLoader.Core 
{
    using System;
    using DNI.Shared.Abstractions;
    using System.Collections.Generic;
    using System.Collections;

    public class AppModuleCache<TAppModuleLoader> : IAppModuleCache<TAppModuleLoader>
        where TAppModuleLoader : class, IAppModuleLoader
    {
        private readonly List<Type> registeredTypes;

        public AppModuleCache()
        {
            registeredTypes = new List<Type>();
        }

        public IEnumerable<Type> RegisteredTypes { get; }

        public IEnumerator<Type> GetEnumerator()
        {
            return registeredTypes.GetEnumerator();
        }

        public void RegisterModule(Type appModuleType)
        {
            registeredTypes.Add(appModuleType);
        }

        public void RegisterModule<TAppModule>()
        {
            RegisterModule(typeof(TAppModule));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}