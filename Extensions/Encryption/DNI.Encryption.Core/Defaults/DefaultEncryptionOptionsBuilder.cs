using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base.Buillders;
using System;

namespace DNI.Encryption.Core.Defaults
{
    public class DefaultEncryptionOptionsBuilder : ModuleOptionsBuilderBase<IEncryptionModuleOptions>, IEncryptionOptionsBuilder
    {
        private readonly DefaultEncryptionModuleOptions encryptionModuleOptions = new();

        public override IEncryptionModuleOptions Build()
        {
            return encryptionModuleOptions;
        }

        public IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure)
        {
            configure?.Invoke(encryptionModuleOptions);
            return this;
        }

        public IEncryptionOptionsBuilder ImportConfiguration(string path = default)
        {
            encryptionModuleOptions.ImportConfiguration = true;
            encryptionModuleOptions.ImportConfigurationPath = path;
            return this;
        }
    }
}
