using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Base
{
    public abstract class AppModuleCacheBase : CollectionBase<Type>, IAppModuleCache
    {
        public IEnumerable<Type> RegisteredTypes => Items;

        public virtual void RegisterModule(Type appModuleType, IConfig config = null)
        {
            Items.Add(appModuleType);
        }

    }
}
