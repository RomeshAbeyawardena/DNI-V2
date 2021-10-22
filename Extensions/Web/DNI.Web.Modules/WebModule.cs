using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using DNI.Shared.Extensions;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Web.Modules
{
    [RequiresDependencies(typeof(DNI.Core.This))]
    public class WebModule : ModuleBase
    {
        private IHost host;
        private readonly ILogger<IModule> logger;
        private readonly IModuleConfiguration moduleConfiguration;
        private IWebModuleOptions options;

        public WebModule(ILogger<IModule> logger, 
            IModuleConfiguration moduleConfiguration, 
            IWebModuleOptions options)
        {
            this.logger = logger;
            this.moduleConfiguration = moduleConfiguration;
            this.options = options;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            moduleConfiguration.ServiceDescriptors = services;

            services
                .AddSingleton(moduleConfiguration.GetOptions<IWebModuleOptions>());
        }

        private void Configure(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddApiVersioning()
                .AddControllers();

            options = moduleConfiguration.GetOptions<IWebModuleOptions>();
            IEnumerable<Assembly> assemblies = options.ToArray();

            options.ConfigureMvcOptions?.Invoke(mvcBuilder);

            if (options.UseModuleAssemblies)
            {
                assemblies = assemblies.AppendMany(moduleConfiguration.GetModuleAssemblies());
            }

            foreach (var moduleAssembly in assemblies.Where(a => a != typeof(WebModule).Assembly))
            {
                mvcBuilder.AddApplicationPart(moduleAssembly);
            }

            StringBuilder serviceLoggerWriter = new("Adding missing services...");
            serviceLoggerWriter.AppendLine($"{services.Count} currently exist...");
            services.ForEach(s => serviceLoggerWriter.AppendLine($"{s}"));
            var distinctServices = moduleConfiguration.ServiceDescriptors.Distinct();

            if (distinctServices.Any())
            {   
                serviceLoggerWriter.AppendLine($"Detected {distinctServices.Count()} to be added.");
            }

            foreach (var service in distinctServices)
            {
                if (service.ServiceType == typeof(IHost))
                    throw new InvalidOperationException();

                if (!services.Contains(service))
                {
                    services.Add(service);
                    serviceLoggerWriter.AppendFormat("\r\n\tAdding {0}...", service);
                }
                else serviceLoggerWriter.AppendLine($"\tDuplicate service: {service}");
            }

            serviceLoggerWriter.AppendLine($"Total of {services.Count} now exist");

            services.Prune();

            logger.LogInformation(serviceLoggerWriter.ToString());

        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(ConfigureWebHost)
                .Build();

            return host.RunAsync(cancellationToken);
        }

        private void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder
                .ConfigureAppConfiguration(c => c
                .AddJsonFile("appsettings.json")
                .AddUserSecrets(options.HostAssembly))
            .ConfigureServices(Configure)
            .Configure(c => c
                    .UseRouting()
                    .UseEndpoints(e => e.MapControllers()));

            options.ConfigureWebHost?.Invoke(webHostBuilder);

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
