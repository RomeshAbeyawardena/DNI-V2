using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class Startup : DisposableStartupBase
    {
        private readonly IModuleStartup moduleStartup;

        public Startup(IModuleStartup moduleStartup)
        {
            this.moduleStartup = moduleStartup;
        }

        public override void Dispose(bool disposing)
        {
            moduleStartup.Dispose();
        }

        public override Task StartAsync(CancellationToken cancellationToken, params object[] args)
        {
            return moduleStartup.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return moduleStartup.StopAsync(cancellationToken);
        }
    }
}
