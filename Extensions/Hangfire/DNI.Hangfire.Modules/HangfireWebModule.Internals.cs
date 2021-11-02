using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .AddHangfire(c => ConfigureService(c))
                .AddHangfireServer();
        }

        private void ConfigureService(IGlobalConfiguration configuration)
        {
            Options.ConfigureHangfire(configuration);
        }
    }
}
