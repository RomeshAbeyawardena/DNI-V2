using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Encryption.Shared.Base;
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
            var encryptedBytes = Convert.FromBase64String(value);
            using (var memoryStream = new MemoryStream(encryptedBytes))
            using (var algorithm = GetSymmetricAlgorithm(encryptionOptions.Algorithm.Value))
            using (var encryptor = algorithm.CreateDecryptor(Convert.FromBase64String(encryptionOptions.Key),
                    Convert.FromBase64String(encryptionOptions.InitialVector)))
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Read))
            using (var streamReader = new StreamReader(cryptoStream, encryptionOptions.Encoding))
            {
               return streamReader.ReadToEnd();
            }
        }
    }
}
