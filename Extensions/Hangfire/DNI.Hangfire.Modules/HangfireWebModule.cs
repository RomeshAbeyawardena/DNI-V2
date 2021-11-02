using DNI.Hangfire.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Hangfire.Modules
{
    public class HangfireWebModule : ModuleBase
    {
        private readonly IModuleConfiguration moduleConfiguration;

        private IHangfireModuleOptions Options => moduleConfiguration.GetOptions<IHangfireModuleOptions>(ModuleDescriptor);

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureWebModule<HangfireWebModule>(configure => configure
                    .ConfigureServices(ConfigureServices)
                    .ConfigureApplicationBuilder(ConfigureAppBuilder)
                    .ConfigureEndpoints(ConfigureEndpoints));
        }

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

        public HangfireWebModule(IModuleConfiguration moduleConfiguration)
        {
            this.moduleConfiguration = moduleConfiguration;
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
