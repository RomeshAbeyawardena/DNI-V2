using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Web.Modules
{
    public class WebModule : ModuleBase
    {
        private IHost host;

        [Resolve] private static IWebModuleOptions Options { get; set; }
        [Resolve] private static IDictionary<Assembly, IAssemblyOptions> AssemblyOptions { get; set; }
        
        [RuntimeBinding(false)]
        public static void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddControllers(Options.ConfigureMvcOptions);

            var assemblies = Options.UseModuleAssemblies 
                ? AssemblyOptions.Select(a => a.Key).ToArray()
                : Options.ToArray();

            foreach (var assembly in assemblies)
            {
                mvcBuilder
                    .AddApplicationPart(assembly);
            }

            mvcBuilder
                .AddControllersAsServices(); 
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            var hostBuilder = Host.CreateDefaultBuilder();

            if (Options.ConfigureWebHost != null)
            {
                hostBuilder.ConfigureWebHostDefaults(ConfigureWebHost);
            }

            CancellationTokenSource cancellationTokenSource = new();
            host = hostBuilder.Build();
            return host.RunAsync(cancellationToken);           //Console.WriteLine(host);
        }

        private void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            Options.ConfigureWebHost?.Invoke(webHostBuilder); 
            webHostBuilder.UseStartup<Startup>();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return host?.StopAsync(cancellationToken);
        }

        public override void Dispose(bool dispose)
        {
            if (dispose)
            {
                host?.Dispose();
            }
        }


        class Startup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                WebModule.ConfigureServices(services);
            }

            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }

                app.UseRouting();

                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });


            }
        }
    }
}
