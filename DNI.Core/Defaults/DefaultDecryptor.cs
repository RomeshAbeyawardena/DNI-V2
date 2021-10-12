using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Shared.Base;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Core.Defaults
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultDecryptor : CryptographicProviderBase, IDecryptor
    {
        public DefaultDecryptor(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory) 
            : base(encryptionOptions, symmetricAlgorithmFactory)
        {
        }

        public string Decrypt(string value, string key)
        {
            return Decrypt(value, EncryptionOptions.UseKey(key));
        }

        public string Decrypt(string value, IEncryptionOptions encryptionOptions)
        {
            using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
            using (var algorithm = GetSymmetricAlgorithm(encryptionOptions.Algorithm))
            using (var encryptor = algorithm.CreateEncryptor())
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream, encryptionOptions.Encoding))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}
