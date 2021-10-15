using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Web
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddWebModule(this IModuleConfigurationBuilder builder)
        {
            return builder.AddModule(typeof(WebModule));
        }
    }
}
