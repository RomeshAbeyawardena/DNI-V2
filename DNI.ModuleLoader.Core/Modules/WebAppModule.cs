using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core.Modules
{
    public class WebAppModule : AppModuleBase
    {
        public WebAppModule(IAppModuleCache appModuleCache)
            : base(appModuleCache)
        {

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args);

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            return CreateHostBuilder(null)
                .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                .ConfigureServices(services => RegisterServices(AppModuleCache, services));
            }).RunConsoleAsync();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            services.AddControllers();
        }

        public override bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }
    }
}
