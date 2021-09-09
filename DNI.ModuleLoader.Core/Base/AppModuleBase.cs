using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Base
{
    public abstract class AppModuleBase : IAppModule
    {
        public AppModuleBase(IAppModuleCache appModuleCache)
        {
            AppModuleCache = appModuleCache;
        }

        public IAppModuleCache AppModuleCache { get; }

        public abstract Task RunAsync(CancellationToken cancellationToken);
        public abstract Task StopAsync(CancellationToken cancellationToken);
        public abstract bool ValidateServices(IServiceProvider serviceProvider);
    }
}
