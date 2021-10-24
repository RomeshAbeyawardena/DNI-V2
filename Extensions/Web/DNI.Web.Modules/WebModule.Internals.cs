using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Shared.Extensions;
using DNI.Web.Core.Defaults.Providers;
using DNI.Web.Shared.Abstractions;
using DNI.Web.Shared.Abstractions.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Web.Modules
{
    public partial class WebModule
    {
        private void ConfigureServices(IServiceCollection services)
        {
            var mvcBuilder = services
                .AddApiVersioning()
                .AddControllers();

            options = moduleConfiguration.GetOptions<IWebModuleOptions>(ModuleDescriptor);
            IEnumerable<Assembly> assemblies = options.ToArray();

            options.ConfigureMvcOptions?.Invoke(mvcBuilder);

            if (options.UseModuleAssemblies)
            {
                assemblies = assemblies.AppendMany(moduleConfiguration.GetModuleAssemblies());
            }

            foreach (var moduleAssembly in assemblies.Where(a => a != typeof(WebModule).Assembly))
            {
                mvcBuilder.AddApplicationPart(moduleAssembly);
            }

            services.AddSingleton<IWebServiceProvider>(s => new DefaultWebServiceProvider(s, moduleConfiguration.ServiceProvider));
        }

        private void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder
                .Configure(Configure);
            
            options.ConfigureWebHost?.Invoke(webHostBuilder);

        }

        private void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseRouting()
                    .UseEndpoints(e => e.MapControllers());
        }
    }
}
