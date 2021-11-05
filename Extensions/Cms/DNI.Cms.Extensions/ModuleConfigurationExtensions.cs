using DNI.Cms.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Extensions;
using System;
using DNI.Cms.Core.Defaults;
using DNI.Cms.Core.Defaults.Builders;

namespace DNI.Cms.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureCmsModuleOptions(this IModuleConfiguration moduleConfiguration,
            IModuleDescriptor moduleDescriptor,
            Type parentType,
            Action<ICmsModuleOptionsBuilder> buildAction)
        {
            var cmsOptions = new DefaultCmsModuleOptionsBuilder();
            buildAction?.Invoke(cmsOptions);
            cmsOptions.SetParentType(parentType);
            return moduleConfiguration.ConfigureOptions(moduleDescriptor, cmsOptions.Build());
        }
    }
}
