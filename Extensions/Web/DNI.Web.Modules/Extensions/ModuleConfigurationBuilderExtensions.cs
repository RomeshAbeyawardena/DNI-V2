using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Web.Extensions;
using DNI.Web.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureWebModule(this IModuleConfigurationBuilder builder, Action<IWebModuleOptionsBuilder> configure)
        {
            builder.AddModule<WebModule>(cfg => cfg.ConfigureWebModule(configure));
            return builder;
        }
    }
}
