using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Base
{
    public abstract class AppModuleCacheBase : CollectionBase<Type>, IAppModuleCache
    {
        private readonly IServiceProvider serviceProvider;
        private List<Assembly> assemblies;

        public AppModuleCacheBase(IServiceProvider serviceProvider)
        {
            assemblies = new List<Assembly>();
            this.serviceProvider = serviceProvider;
        }

        public IEnumerable<Type> RegisteredTypes => Items;

        public IEnumerable<Assembly> RegisteredAssemblies => assemblies;

        public void RegisterAssembly(Assembly assembly)
        {
            assemblies.Add(assembly);
        }

        public virtual void RegisterModule(Type appModuleType, IConfig config = null, Action<IAppModuleCache> appModuleCacheBuilder = null)
        {
            var appModuleCacheType = typeof(IAppModuleCache<>);
            var appModuleCache = serviceProvider.GetService(appModuleCacheType.MakeGenericType(appModuleType)) as IAppModuleCache;
            appModuleCacheBuilder?.Invoke(appModuleCache);
            Items.Add(appModuleType);
        }

    }
}
