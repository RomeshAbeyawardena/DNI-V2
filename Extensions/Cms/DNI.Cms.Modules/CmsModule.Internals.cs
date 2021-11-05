using Microsoft.AspNetCore.Builder;
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
    public partial class CmsModule
    {
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddUmbraco(webHostEnvironment, configuration)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .Build();
        }

        private void ConfigureAppBuilder(IApplicationBuilder app)
        {
            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    Options.ConfigureMiddleware?.Invoke(u);
                    u.UseBackOffice();
                    
                    if (Options.EnableWebsite)
                    {
                        u.UseWebsite();
                    }
                })
                .WithEndpoints(u =>
                {
                    Options.ConfigureEndpoints?.Invoke(u);
                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    if (Options.EnableWebsite)
                    {
                        u.UseWebsiteEndpoints();
                    }
                });
        }
    }
}
