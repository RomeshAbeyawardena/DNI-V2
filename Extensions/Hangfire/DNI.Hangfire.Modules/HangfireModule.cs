using DNI.Hangfire.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Hangfire.Modules
{
    public class HangfireModule : ModuleBase
    {
        private readonly IModuleConfiguration moduleConfiguration;

        private IHangfireModuleOptions Options => moduleConfiguration.GetOptions<IHangfireModuleOptions>(ModuleDescriptor);

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureWebModule<HangfireModule>(configure => configure
                    .ConfigureServices(ConfigureServices)
                    .ConfigureApplicationBuilder(ConfigureAppBuilder));
        }

        private void ConfigureAppBuilder(IApplicationBuilder applicationBuilder)
        {
            if (Options.UseHangfireDashboard)
            {
                applicationBuilder
                    .UseHangfireDashboard(Options.PathMatch ?? "/hangfire", Options.DashboardOptions, Options.JobStorage);
            }
        }

        public HangfireModule(IModuleConfiguration moduleConfiguration)
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
