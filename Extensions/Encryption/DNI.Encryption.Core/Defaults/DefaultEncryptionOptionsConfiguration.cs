using DNI.Core.Defaults;
using DNI.Encryption.Shared.Abstractions;
using Microsoft.Extensions.Configuration;

namespace DNI.Encryption.Core.Defaults
{
    public class DefaultEncryptionOptionsConfiguration : IEncryptionOptionsConfiguration
    {
        public DefaultEncryptionOptionsConfiguration(IConfiguration configuration)
        {
            configuration.Bind(this);
        }

        public string Key { get; set; }
        public string InitialVector { get; set; }
        public string Encoding { get; set; }
        public string SymmetricAlgorithm { get; set; }
        public string HashAlgorithm { get; set; }

        public IEncryptionOptions Build()
        {
            return new DefaultEncryptionOptions(this);
        }
    }
}
