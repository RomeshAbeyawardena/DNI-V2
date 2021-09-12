using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
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
#pragma warning disable 0649 //Should always be injected 
        private IModuleServiceProvider moduleServiceProvider = null;
#pragma warning disable 0649

        protected TResult BeginScope<TService, TResult>(Func<TService, TResult> transaction)
        {
            using var scope = ServiceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TService>();
            return transaction.Invoke(service);
        }

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
