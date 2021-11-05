using DNI.Encryption.Core.Defaults;
using DNI.Encryption.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Encryption.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureEncryptionModuleOptions(this IModuleConfiguration moduleConfiguration, 
            IModuleDescriptor moduleDescriptor, Action<IEncryptionOptionsBuilder> configure)
        {
            var encryptionOptionsBuilder = new DefaultEncryptionOptionsBuilder();
            configure(encryptionOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, encryptionOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
