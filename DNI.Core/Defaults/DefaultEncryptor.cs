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
    public class DefaultEncryptor : CryptographicProviderBase, IEncryptor
    {
        public DefaultEncryptor(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory) 
            : base(encryptionOptions, symmetricAlgorithmFactory)
        {
            
        }

        public string Encrypt(string value)
        {
            using (var memoryStream = new MemoryStream())
            using (var algorithm = GetSymmetricAlgorithm(EncryptionOptions.Algorithm))
            using (var encryptor = algorithm.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream, EncryptionOptions.Encoding))
            {
                streamWriter.Write(value);
                return Convert.ToBase64String(memoryStream.ToArray());
            }

        }
    }
}
