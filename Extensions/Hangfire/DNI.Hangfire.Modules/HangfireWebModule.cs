using DNI.Hangfire.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Hangfire.Modules
{
    public partial class HangfireWebModule : ModuleBase
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

        public HangfireWebModule(IModuleConfiguration moduleConfiguration)
        {
            this.moduleConfiguration = moduleConfiguration;
        }

    }
}
