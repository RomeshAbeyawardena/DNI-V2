using DNI.Shared.Base;
using DNI.Web.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace DNI.Web.Core.Defaults
{
    public class DefaultWebModuleOptions : CollectionBase<Assembly>, IWebModuleOptions
    {
        public DefaultWebModuleOptions(Action<IWebHostBuilder> configureWebHost, 
            Action<MvcOptions> configureMvcOptions, bool useModuleAssemblies)
        {
            ConfigureWebHost = configureWebHost;
            ConfigureMvcOptions = configureMvcOptions;
            UseModuleAssemblies = useModuleAssemblies;
        }
        
        public Action<IWebHostBuilder> ConfigureWebHost { get; }

        public Action<MvcOptions> ConfigureMvcOptions { get; }

        public bool UseModuleAssemblies { get; }
    }
}
