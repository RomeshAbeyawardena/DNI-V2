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
        private readonly ILogger<Startup> logger;

        public Startup(IModuleStartup moduleStartup,
            ILogger<Startup> logger)
        {
            this.moduleStartup = moduleStartup;
            this.logger = logger;
        }

        public override void Dispose(bool disposing)
        {
            logger.LogInformation("Dispose called");
            if (disposing)
            {
                moduleStartup.Dispose();
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken = default, params object[] args)
        {
            logger.LogInformation("StartAsync called on {0}", Thread.CurrentThread.ManagedThreadId);
            return moduleStartup.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync called");
            return moduleStartup.StopAsync(cancellationToken);
            //return Task.CompletedTask;
        }
    }
}
