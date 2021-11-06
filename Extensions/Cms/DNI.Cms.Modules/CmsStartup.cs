using DNI.Cms.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions;
using DNI.Web.Shared.Abstractions.Providers;
using DNI.Web.Shared.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Extensions;

namespace DNI.Cms.Modules
{
    public class CmsStartup : Web.Shared.Base.StartupBase
    {
        //private ICmsModuleOptions Options { get; }
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IConfiguration configuration;

        public CmsStartup(
            IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            //this.Options = cmsModuleOptions;
            this.webHostEnvironment = webHostEnvironment;
            this.configuration = configuration;
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.AddUmbraco(webHostEnvironment, configuration)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .Build();
        }

        public override void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var options = app.ApplicationServices.GetService(typeof(ICmsModuleOptions));
            app.UseUmbraco()
               .WithMiddleware(u =>
               {
                   //Options.ConfigureMiddleware?.Invoke(u);
                   u.UseBackOffice();

                   //if (Options.EnableWebsite)
                   //{
                       u.UseWebsite();
                   //}
               })
               .WithEndpoints(u =>
               {
                   //Options.ConfigureEndpoints?.Invoke(u);
                   u.UseInstallerEndpoints();
                   u.UseBackOfficeEndpoints();
                   //if (Options.EnableWebsite)
                   //{
                       u.UseWebsiteEndpoints();
                   //}
               });
        }
    }
}
