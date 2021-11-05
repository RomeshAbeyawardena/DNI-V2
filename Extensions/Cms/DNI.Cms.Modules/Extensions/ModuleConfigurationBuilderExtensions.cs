using DNI.Cms.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Web.Modules.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Cms.Extensions;

namespace DNI.Cms.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureCmsModule<T>(this IModuleConfigurationBuilder moduleConfigurationBuilder, Action<ICmsModuleOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder.ConfigureCmsModule(typeof(T), configure);
        }

        public static IModuleConfigurationBuilder ConfigureCmsModule(this IModuleConfigurationBuilder moduleConfigurationBuilder, Type parentType, Action<ICmsModuleOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder.AddModule<CmsModule>((moduleDescriptor, cfg) => cfg.ConfigureCmsModuleOptions(moduleDescriptor, parentType, configure));
        }
    }
}
