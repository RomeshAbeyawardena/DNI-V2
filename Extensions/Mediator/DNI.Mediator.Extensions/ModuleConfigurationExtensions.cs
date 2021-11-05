using DNI.Mediator.Core.Defaults;
using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Mediator.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureMediatorModuleOptions(
            this IModuleConfiguration moduleConfiguration,
            IModuleDescriptor moduleDescriptor,
            Action<IMediatorModuleOptionsBuilder> buildAction)
        {
            IMediatorModuleOptionsBuilder defaultMediatorModuleOptionsBuilder = new DefaultMediatorModuleOptionsBuilder();
            buildAction?.Invoke(defaultMediatorModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, defaultMediatorModuleOptionsBuilder.Build());

            return moduleConfiguration;
        }
    }
}
