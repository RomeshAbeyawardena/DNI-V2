using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using System.Collections.Generic;

namespace DNI.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultEncryptionOptionsFactory : IEncryptionOptionsFactory
    {
        private readonly IDictionary<string, IEncryptionOptions> encryptionOptions;

        public DefaultEncryptionOptionsFactory(
            IDictionary<string, IEncryptionOptions> encryptionOptions)
        {
            this.encryptionOptions = encryptionOptions;
        }

        public IEncryptionOptions GetEncryptionOptions(string sectionName)
        {
            return new DefaultEncryptionOptions(encryptionOptions, sectionName);

        }
    }
}
