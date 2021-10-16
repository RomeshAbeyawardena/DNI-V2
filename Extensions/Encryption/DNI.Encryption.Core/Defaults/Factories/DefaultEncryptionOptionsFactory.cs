using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
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
        private readonly IConfiguration configuration;
        private readonly IDictionary<string, IEncryptionOptions> encryptionOptions;

        public DefaultEncryptionOptionsFactory(
            IConfiguration configuration,
            IDictionary<string, IEncryptionOptions> encryptionOptions)
        {
            this.configuration = configuration;
            this.encryptionOptions = encryptionOptions;
        }

        public IEncryptionOptions GetEncryptionOptions(string sectionName, string path)
        {
            if(encryptionOptions != null)
            {
                return new DefaultEncryptionOptions(encryptionOptions, sectionName);
            }

            throw new NotSupportedException();
        }
    }
}
