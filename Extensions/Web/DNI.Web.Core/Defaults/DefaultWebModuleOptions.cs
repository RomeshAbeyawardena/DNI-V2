using DNI.Shared.Base;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Web.Core.Defaults
{
    public class DefaultWebModuleOptions : CollectionBase<Assembly>, IWebModuleOptions
    {

        public DefaultWebModuleOptions(Assembly hostAssembly)
        {
            HostAssembly = hostAssembly;
        }

        public Action<IEndpointRouteBuilder> ConfigureEndpoints { get; init; }

        public Action<IApplicationBuilder> ConfigureApplicationBuilder { get; init; }

        public Action<IWebHostBuilder> ConfigureWebHost { get; init; }

        public Action<IMvcBuilder> ConfigureMvcOptions { get; init; }

        public Action<IServiceCollection> ConfigureServices { get; init; }

        public bool UseModuleAssemblies { get; init; }

        public bool UseStartup { get; init; }

        public Type StartupType { get; init; }

        public Assembly HostAssembly { get; }
    }
}
