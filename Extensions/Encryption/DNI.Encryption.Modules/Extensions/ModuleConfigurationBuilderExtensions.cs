using DNI.Encryption.Extensions;
using DNI.Encryption.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Encryption.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureEncryptionModule(this IModuleConfigurationBuilder moduleConfigurationBuilder,
            Action<IEncryptionOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder
                .AddModule<EncryptionModule>((moduleDescriptor, configuration) => configuration.ConfigureEncryptionModule(moduleDescriptor, configure));
        }
    }
}
