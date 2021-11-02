using DNI.Extensions;
using DNI.Modules.Extensions;
using DNI.Shared.Extensions;
using DNI.Web.Core.Defaults.Providers;
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
            Options.ConfigureServices?.Invoke(services);

            var mvcBuilder = services
                .AddApiVersioning()
                .AddControllers();

            IEnumerable<Assembly> assemblies = Options.ToArray();
            Options.ConfigureMvcOptions?.Invoke(mvcBuilder);

            if (Options.UseModuleAssemblies)
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

            Options.ConfigureWebHost?.Invoke(webHostBuilder);

        }

        private void Configure(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.UseRouting();

            Options.ConfigureApplicationBuilder?.Invoke(applicationBuilder);

            applicationBuilder.UseEndpoints(e => { e.MapControllers(); Options.ConfigureEndpoints?.Invoke(e); });
        }
    }
}
