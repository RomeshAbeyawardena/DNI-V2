namespace DNI.ModuleLoader.Core 
{
    using DNI.Shared.Abstractions;
    using System.Collections.Generic;

    public class AppModuleCache : IAppModuleCache
    {
        private readonly List<Type> registeredTypes;

        public AppModuleCache()
        {
            registeredTypes = new List<Type>();
        }

        public IEnumerable<Type> RegisteredTypes { get; }

        public void RegisterModule(Type appModuleType)
        {
            registeredTypes.Add(appModuleType);
        }
    }
}