using DNI.Cms.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Web.Modules.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Cms.Modules
{
    public partial class CmsModule : ModuleBase<ICmsModuleOptions>
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public CmsModule(IModuleConfiguration moduleConfiguration,
            IWebHostEnvironment webHostEnvironment, 
            IConfiguration configuration) 
            : base(moduleConfiguration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder.ConfigureWebModule(Options.ParentType, configure => configure
                .ConfigureServices(ConfigureServices)
                .ConfigureApplicationBuilder(ConfigureAppBuilder));
        }

    }
}
