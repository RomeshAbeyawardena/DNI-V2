using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions
{
    public interface IWebModuleOptions : IEnumerable<Assembly>
    {
        Action<IWebHostBuilder> ConfigureWebHost { get; }
        Action<MvcOptions> ConfigureMvcOptions { get; }
        bool UseModuleAssemblies { get; }
    }
}
