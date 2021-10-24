using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
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
    public partial class WebModule : ModuleBase
    {
        private IHost host;
        private readonly ILogger<IModule> logger;
        private readonly IModuleConfiguration moduleConfiguration;
        private IWebModuleOptions options;

        public WebModule(
            ILogger<IModule> logger, 
            IModuleConfiguration moduleConfiguration, 
            IWebModuleOptions options)
        {
            this.logger = logger;
            this.moduleConfiguration = moduleConfiguration;
            this.options = options;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.ServiceDescriptors = services.ToArray();

            services
                .AddSingleton(moduleConfiguration.GetOptions<IWebModuleOptions>(ModuleDescriptor));
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(c => c
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets(options.HostAssembly))
                .ConfigureServices(ConfigureServices)
                .ConfigureWebHostDefaults(ConfigureWebHost)
                .Build();
                
            return host.RunAsync(cancellationToken);
        }

        public override void OnDispose(bool disposing)
        {
            host.Dispose();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return host.StopAsync(cancellationToken);
        }
    }
}
