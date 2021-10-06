using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Web.Modules
{
    public class WebModule : ModuleBase
    {
        private IHost host;

        [Resolve] private static IWebModuleOptions Options { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(Options.ConfigureMvcOptions);
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices(ConfigureServices)
                .ConfigureWebHostDefaults(Options.ConfigureWebHost)
                .Build();

            return host.StartAsync(cancellationToken);
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return host.StopAsync(cancellationToken);
        }

        public override void Dispose(bool dispose)
        {
            if (dispose)
            {
                host.Dispose();
            }
        }
    }
}
