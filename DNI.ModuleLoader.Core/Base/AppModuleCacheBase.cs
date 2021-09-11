using DNI.Shared.Abstractions;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Base
{
    public abstract class AppModuleCacheBase : IAppModuleCache
    {
        private readonly ConcurrentBag<Type> registeredTypes;

        public AppModuleCacheBase()
        {
            registeredTypes = new ConcurrentBag<Type>();
        }

        public IEnumerable<Type> RegisteredTypes { get; }

        public IEnumerator<Type> GetEnumerator()
        {
            return registeredTypes.GetEnumerator();
        }

        public virtual void RegisterModule(Type appModuleType)
        {
            registeredTypes.Add(appModuleType);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
