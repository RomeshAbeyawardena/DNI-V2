using DNI.Modules.Shared.Builders;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Web.Shared.Abstractions.Builders
{
    public interface IWebModuleOptionsBuilder : IModuleOptionsAssemblyBuilder<IWebModuleOptions>
    {
        new IWebModuleOptionsBuilder AddAssembly<T>();
        new IWebModuleOptionsBuilder AddAssembly(Type type);
        new IWebModuleOptionsBuilder AddAssembly(Assembly assembly);
        IWebModuleOptionsBuilder ConfigureWebHost(Action<IWebHostBuilder> configureAction);
        IWebModuleOptionsBuilder ConfigureMvcOptions(Action<IMvcBuilder> mvcOptions);
        IWebModuleOptionsBuilder ConfigureServices(Action<IServiceCollection> services);
        IWebModuleOptionsBuilder UseDefaultMvcOptions();
        IWebModuleOptionsBuilder AddModuleAssemblies();
    }
}
