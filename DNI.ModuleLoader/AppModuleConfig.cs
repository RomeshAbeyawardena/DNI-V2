using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Shared.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader
{
    [ServiceDiscovery(false)]
    public class AppModuleConfig<TAppModule> : CollectionBase<Type>, IAppModuleConfig<TAppModule>
        where TAppModule : class, IAppModule
    {
        public IAppModuleConfig<TAppModule> AddConfiguration(Type configType)
        {
            Items.Add(configType);
            return this;
        }

        IAppModuleConfig<TAppModule> IAppModuleConfig<TAppModule>.AddConfiguration<TConfiguration>()
        {
            return AddConfiguration(typeof(TConfiguration));
        }
    }
}
