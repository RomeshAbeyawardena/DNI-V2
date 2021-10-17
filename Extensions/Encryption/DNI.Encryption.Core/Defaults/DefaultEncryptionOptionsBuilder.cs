using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Builders;
using System;

namespace DNI.Encryption.Core.Defaults
{
    public class DefaultEncryptionOptionsBuilder : IEncryptionOptionsBuilder
    {
        private readonly DefaultEncryptionModuleOptions encryptionModuleOptions = new();

        public IEncryptionModuleOptions Build()
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
