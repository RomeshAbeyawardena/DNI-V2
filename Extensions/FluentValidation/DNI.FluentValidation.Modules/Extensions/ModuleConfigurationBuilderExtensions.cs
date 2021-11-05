using DNI.FluentValidation.Extensions;
using DNI.FluentValidation.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.FluentValidation.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureFluentValidation(this IModuleConfigurationBuilder moduleConfigurationBuilder,
            Action<IFluentValidationModuleOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder.AddModule<FluentValidationModule>((moduleDescriptor, moduleConfiguration) => moduleConfiguration.ConfigureFluentValidationOptions(moduleDescriptor, configure));
        }
    }
}
