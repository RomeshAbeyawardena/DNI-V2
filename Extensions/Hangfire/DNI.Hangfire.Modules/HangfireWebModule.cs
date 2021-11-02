using DNI.Hangfire.Core;
using DNI.Hangfire.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Shared.Attributes;
using DNI.Web.Modules.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Hangfire.Modules
{
    [RequiresDependencies(typeof(This))]
    public partial class HangfireWebModule : ModuleBase<IHangfireModuleOptions>
    {
        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureWebModule(Options.ParentType, configure => configure
                    .ConfigureServices(ConfigureServices)
                    .ConfigureApplicationBuilder(ConfigureAppBuilder)
                    .ConfigureWebHost(w => Options.ConfigureWebHost?.Invoke(w))
                    .ConfigureEndpoints(ConfigureEndpoints));
        }

        public HangfireWebModule(IModuleConfiguration moduleConfiguration)
            : base(moduleConfiguration)
        {
        }
    }
}
