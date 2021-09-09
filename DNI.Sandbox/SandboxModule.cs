using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    public class SandboxModule : IAppModule
    {
        public SandboxModule(IFileProvider fileProvider)
        {

        }

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
