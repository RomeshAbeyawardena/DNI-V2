using DNI.Cms.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Web.Modules.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Cms.Modules
{
    public partial class CmsModule : ModuleBase<ICmsModuleOptions>
    {
        public CmsModule(IModuleConfiguration moduleConfiguration) 
            : base(moduleConfiguration)
        {
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureWebModule(Options.ParentType, configure => configure
                    .ConfigureServices(ConfigureServices)
                    .ConfigureWebHost(ConfigureWebHost)
                    .UseStartup<CmsStartup>());
        }

    }
}
