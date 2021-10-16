using DNI.Modules.Extensions;
using DNI.Encryption.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Encryption.Extensions;

namespace DNI.Encryption.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureEncryptionModule(this IModuleConfigurationBuilder moduleConfigurationBuilder, 
            Action<IEncryptionOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder
                .AddModule<EncryptionModule>(configuration => configuration.ConfigureEncryptionModule(configure));
        }
    }
}
