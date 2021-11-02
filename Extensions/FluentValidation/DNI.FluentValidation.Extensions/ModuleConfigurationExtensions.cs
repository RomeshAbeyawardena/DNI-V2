using DNI.FluentValidation.Core.Defaults.Builders;
using DNI.FluentValidation.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.FluentValidation.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureFluentValidation(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor, Action<IFluentValidationModuleOptionsBuilder> buildAction) 
        {
            var fluentValidationOptionsBuilder = new DefaultFluentValidationModuleOptionsBuilder();
            buildAction?.Invoke(fluentValidationOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, fluentValidationOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
