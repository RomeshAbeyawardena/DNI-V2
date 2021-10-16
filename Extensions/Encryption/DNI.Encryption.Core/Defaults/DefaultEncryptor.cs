using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Encryption.Shared.Base;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Encryption.Core.Defaults
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
            var encryptedBytes = ExecuteSymmetricOperation(Shared.Enumerations.EncryptionMode.Encrypt, 
                encryptionOptions, CryptoStreamMode.Write,
                (m, c) => { 
                    using (var streamWriter = new StreamWriter(c, encryptionOptions.Encoding)) 
                        streamWriter.Write(value);
                    return m;
                }).ToArray();


            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
