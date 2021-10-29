using DNI.Shared.Base;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Web.Core.Defaults
{
    public class DefaultWebModuleOptions : CollectionBase<Assembly>, IWebModuleOptions
    {
        public DefaultWebModuleOptions(Action<IWebHostBuilder> configureWebHost,
            Action<IMvcBuilder> configureMvcOptions, Action<IServiceCollection> configureServices,
            bool useModuleAssemblies, Assembly hostAssembly)
        {
            ConfigureWebHost = configureWebHost;
            ConfigureMvcOptions = configureMvcOptions;
            ConfigureServices = configureServices;
            UseModuleAssemblies = useModuleAssemblies;
            HostAssembly = hostAssembly;
        }

        public Action<IWebHostBuilder> ConfigureWebHost { get; }

        public Action<IMvcBuilder> ConfigureMvcOptions { get; }

        public Action<IServiceCollection> ConfigureServices { get; }

        public bool UseModuleAssemblies { get; }

        public Assembly HostAssembly { get; }
    }
}
