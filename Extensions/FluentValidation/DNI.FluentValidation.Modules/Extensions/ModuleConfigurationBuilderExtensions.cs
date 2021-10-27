using DNI.FluentValidation.Extensions;
using DNI.FluentValidation.Modules;
using DNI.FluentValidation.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentValidation.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddFluentValidation(this IModuleConfigurationBuilder moduleConfigurationBuilder, 
            Action<IFluentValidationModuleOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder.AddModule<FluentValidationModule>((moduleDescriptor, moduleConfiguration) => moduleConfiguration.ConfigureFluentValidation(moduleDescriptor, configure));
        }
    }
}
