using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Web.Modules
{
    [RequiresDependencies(typeof(DNI.Core.This))]
    public partial class WebModule : ModuleBase<IWebModuleOptions>
    {
        private IHost host;
        private readonly ILogger<WebModule> logger;

        public WebModule(
            IModuleConfiguration moduleConfiguration,
            ILogger<WebModule> logger)
            : base(moduleConfiguration)
        {
            this.logger = logger;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.ServiceDescriptors = services.ToArray();
        }

        public override async Task OnStart(CancellationToken cancellationToken)
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => c
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets(Options.HostAssembly))
                .ConfigureServices(ConfigureServices)
                .ConfigureWebHostDefaults(ConfigureWebHost)
                .Build();

            logger.LogTrace("Running module {0}:", ModuleDescriptor.Id);

            await host.RunAsync(cancellationToken);
        }

        public override void OnDispose(bool disposing)
        {
            host?.Dispose();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return host?.StopAsync(cancellationToken);
        }
    }
}
