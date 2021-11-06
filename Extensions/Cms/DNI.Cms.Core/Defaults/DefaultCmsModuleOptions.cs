using DNI.Cms.Shared.Abstractions;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace DNI.Cms.Core.Defaults
{
    public class DefaultCmsModuleOptions : ICmsModuleOptions
    {
        public bool EnableWebsite { get; init; }

        public Type ParentType { get; init; }

        public Action<IUmbracoApplicationBuilderContext> ConfigureMiddleware { get; init; }

        public Action<IUmbracoEndpointBuilderContext> ConfigureEndpoints { get; init; }

        public Action<IWebHostBuilder> ConfigureWebHost { get; init; }
    }
}
