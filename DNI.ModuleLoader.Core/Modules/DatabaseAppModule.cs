using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Modules
{
    public class DatabaseAppModule : IAppModule
    {
        public IServiceProvider ServiceProvider => throw new NotImplementedException();

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {

        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool ValidateServices(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}
