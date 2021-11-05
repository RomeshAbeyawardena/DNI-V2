using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace DNI.Cms.Shared.Abstractions
{
    public interface ICmsModuleOptions
    {
        bool EnableWebsite { get; }
        Type ParentType { get; }
        Action<IUmbracoApplicationBuilderContext> ConfigureMiddleware { get; }
        Action<IUmbracoEndpointBuilderContext> ConfigureEndpoints { get; }
    }
}
