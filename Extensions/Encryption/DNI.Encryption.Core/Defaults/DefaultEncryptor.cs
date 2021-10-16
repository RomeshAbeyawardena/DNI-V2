using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Encryption.Shared.Base;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Core.Defaults
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultEncryptor : CryptographicProviderBase, IEncryptor
    {
        public DefaultEncryptor(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory) 
            : base(encryptionOptions, symmetricAlgorithmFactory)
        {
            
        }

        public string Encrypt(string value, string key)
        {
            return Encrypt(value, EncryptionOptions.UseKey(key));
        }

        public string Encrypt(string value, IEncryptionOptions encryptionOptions)
        {
            using (var memoryStream = new MemoryStream())
            using (var algorithm = GetSymmetricAlgorithm(encryptionOptions.Algorithm.Value))
            using (var encryptor = algorithm.CreateEncryptor(
                Convert.FromBase64String(encryptionOptions.Key),
                Convert.FromBase64String(encryptionOptions.InitialVector)))
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            using (var streamWriter = new StreamWriter(cryptoStream, encryptionOptions.Encoding))
            {
                streamWriter.Write(value);
                return Convert.ToBase64String(memoryStream.ToArray());
            }
        }
    }
}
