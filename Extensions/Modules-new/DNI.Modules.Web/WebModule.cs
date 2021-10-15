﻿using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Web
{
    public class WebModule : ModuleBase
    {
        private IHost host;
        private readonly IServiceCollection services;

        public WebModule(IServiceCollection services)
        {
            this.services = services;
        }

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var mvcBuilder = services.AddControllers();
            foreach (var moduleAssembly in moduleConfiguration.ModuleTypes.Select(a => a.Assembly).Distinct())
            {
                mvcBuilder.AddApplicationPart(moduleAssembly)
                    .AddControllersAsServices();
            }

            services.AddSingleton(services);
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
            webHostBuilder.ConfigureServices(s => services.ForEach(sv => s.Add(sv)))
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
