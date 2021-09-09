using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Modules
{
    public class MyService { }

    public class BuiltinAppModule : IAppModule
    {
        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            services.AddSingleton<MyService>();
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }
    }
}
