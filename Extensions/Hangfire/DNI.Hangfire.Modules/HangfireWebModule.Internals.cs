using DNI.Hangfire.Core.Defaults;
using DNI.Hangfire.Shared.Abstractions;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Hangfire.Modules
{
    public partial class HangfireWebModule
    {
        
        private void ConfigureEndpoints(IEndpointRouteBuilder endpointRouteBuilder)
        {
            if (Options.UseHangfireDashboard)
            {
                endpointRouteBuilder.MapHangfireDashboard();
            }

        }

        private void ConfigureAppBuilder(IApplicationBuilder applicationBuilder)
        {
            if (Options.UseHangfireDashboard)
            {
                applicationBuilder
                    .UseHangfireDashboard(Options.PathMatch ?? "/hangfire", Options.DashboardOptions, Options.JobStorage);
            }
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IJobActivator, DefaultJobActivator>()
                .AddHangfire(ConfigureService)
                .AddHangfireServer();
        }

        private void ConfigureService(IServiceProvider serviceProvider, IGlobalConfiguration configuration)
        {
            var jobActivator = serviceProvider.GetRequiredService<IJobActivator>();
            configuration.UseActivator(jobActivator.JobActivator);
            Options.ConfigureHangfire?.Invoke(configuration);
        }
    }
}
