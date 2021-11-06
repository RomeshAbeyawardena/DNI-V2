using DNI.Modules.Shared.Builders;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace DNI.Cms.Shared.Abstractions.Builders
{
    public interface ICmsModuleOptionsBuilder : IModuleOptionsAssemblyBuilder<ICmsModuleOptions>
    {
        ICmsModuleOptionsBuilder ConfigureMiddleware(Action<IUmbracoApplicationBuilderContext> configureMiddleware);
        ICmsModuleOptionsBuilder ConfigureEndpoints(Action<IUmbracoEndpointBuilderContext> configureEndpoints);
        ICmsModuleOptionsBuilder ConfigureWebHost(Action<IWebHostBuilder> configureWebHostBuilder);
        ICmsModuleOptionsBuilder SetParentType(Type type);
        ICmsModuleOptionsBuilder EnableWebsite();

        new ICmsModuleOptionsBuilder AddAssembly<T>();
        new ICmsModuleOptionsBuilder AddAssembly(Type type);
        new ICmsModuleOptionsBuilder AddAssembly(Assembly assembly);
    }
}
