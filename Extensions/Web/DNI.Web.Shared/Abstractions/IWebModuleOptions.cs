using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions
{
    public interface IWebModuleOptions : IEnumerable<Assembly>
    {
        Action<IServiceCollection> ConfigureServices { get; }
        Action<IWebHostBuilder> ConfigureWebHost { get; }
        Action<IMvcBuilder> ConfigureMvcOptions { get; }
        bool UseModuleAssemblies { get; }
        Assembly HostAssembly { get; }
    }
}
