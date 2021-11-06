using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        private void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            Options.ConfigureWebHost?.Invoke(webHostBuilder);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Options);
        }
    }
}
