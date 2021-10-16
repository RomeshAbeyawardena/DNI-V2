using DNI.Modules.Shared.Abstractions;

using System;
using DNI.Encryption.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Core.Defaults;

namespace DNI.Encryption.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureEncryptionModule(this IModuleConfiguration moduleConfiguration, Action<IEncryptionOptionsBuilder> configure)
        {
            var encryptionOptionsBuilder = new DefaultEncryptionOptionsBuilder();
            configure(encryptionOptionsBuilder);
            moduleConfiguration.ConfigureOptions(encryptionOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
