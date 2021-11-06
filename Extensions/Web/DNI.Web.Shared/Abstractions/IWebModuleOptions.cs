using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions
{
    public interface IWebModuleOptions : IEnumerable<Assembly>
    {
        Action<IApplicationBuilder> ConfigureApplicationBuilder { get; }
        Action<IServiceCollection> ConfigureServices { get; }
        Action<IWebHostBuilder> ConfigureWebHost { get; }
        Action<IMvcBuilder> ConfigureMvcOptions { get; }
        bool UseModuleAssemblies { get; }
        bool UseStartup { get; }
        Type StartupType { get; }
        Assembly HostAssembly { get; }
        Action<IEndpointRouteBuilder> ConfigureEndpoints { get; }
    }
}
