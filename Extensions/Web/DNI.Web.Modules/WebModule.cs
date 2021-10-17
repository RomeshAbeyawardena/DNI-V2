using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Web.Modules
{
    [RequiresDependencies(typeof(DNI.Core.This))]
    public class WebModule : ModuleBase
    {
        private IHost host;
        private readonly IServiceCollection services;
        private readonly IWebModuleOptions options;

        public WebModule(IServiceCollection services, IWebModuleOptions options)
        {
            this.services = services;
            this.options = options;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var mvcBuilder = services.AddControllers();

            var options = moduleConfiguration.GetOptions<IWebModuleOptions>();

            if (options.UseModuleAssemblies)
            {
                foreach (var moduleAssembly in moduleConfiguration.GetModuleAssemblies())
                {
                    mvcBuilder.AddApplicationPart(moduleAssembly)
                        .AddControllersAsServices();
                }
            }

            services.AddSingleton(options);
            services.AddSingleton(services);
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(ConfigureWebHost)
                .ConfigureHostConfiguration(a => a
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets(options.HostAssembly))
                .Build();
            return host.RunAsync(cancellationToken);
        }

        private void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureAppConfiguration(c => c
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(options.HostAssembly))
            .ConfigureServices(s => services.ForEach(sv => s.Add(sv)))
                .Configure(c => c.UseRouting().UseEndpoints(e => e.MapControllers()));
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
