using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultDecryptor : CryptographicProviderBase, IDecryptor
    {
        public DefaultDecryptor(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory) 
            : base(encryptionOptions, symmetricAlgorithmFactory)
        {
        }

        public string Decrypt(string value)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            using (var algorithm = GetSymmetricAlgorithm(EncryptionOptions.Algorithm))
            using (var encryptor = algorithm.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream, EncryptionOptions.Encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
