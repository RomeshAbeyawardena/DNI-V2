using DNI.Mediator.Extensions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Mediator.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureMediatorModule(this IModuleConfigurationBuilder builder, Action<IMediatorModuleOptionsBuilder> configure)
        {
            return builder.AddModule<MediatorModule>((moduleDescriptor, configuration) => configuration
                .ConfigureMediatorModule(moduleDescriptor, configure));
        }
    }
}
