using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Base
{
    public abstract class AppModuleBase<TAppModule> : IAppModule
        where TAppModule : class, IAppModule
    {
        private IModuleServiceProvider moduleServiceProvider;
        public AppModuleBase(IAppModuleCache<TAppModule> appModuleCache)
        {
            AppModuleCache = appModuleCache;
        }

        public IAppModuleCache AppModuleCache { get; }

        public IServiceProvider ServiceProvider => moduleServiceProvider;

        public abstract Task RunAsync(CancellationToken cancellationToken);
        public abstract Task StopAsync(CancellationToken cancellationToken);
        public abstract bool ValidateServices(IServiceProvider serviceProvider);
    }
}
