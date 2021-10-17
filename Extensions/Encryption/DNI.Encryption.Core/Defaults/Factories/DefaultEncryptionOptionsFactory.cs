using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Extensions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
