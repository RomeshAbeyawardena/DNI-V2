using DNI.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions.Builders
{
    public interface IWebModuleOptionsBuilder : IAssemblyOptionsBuilder
    {
        new IWebModuleOptionsBuilder AddAssembly<T>();
        new IWebModuleOptionsBuilder AddAssembly(Type type);
        new IWebModuleOptionsBuilder AddAssembly(Assembly assembly);
        IWebModuleOptionsBuilder ConfigureWebHost(Action<IWebHostBuilder> configureAction);
        IWebModuleOptionsBuilder ConfigureMvcOptions(Action<MvcOptions> mvcOptions);
        IWebModuleOptionsBuilder UseDefaultMvcOptions();
        IWebModuleOptionsBuilder AddModuleAssemblies();
        new IWebModuleOptions Build();
    }
}
