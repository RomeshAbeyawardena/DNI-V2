using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Base;
using DNI.Shared.Attributes;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Encryption.Core.Defaults
{
    [RegisterService(Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton)]
    public class DefaultEncryptor : CryptographicProviderBase, IEncryptor
    {
        private readonly IEncryptionModuleOptions encryptionModuleOptions;

        public DefaultEncryptor(
            IEncryptionModuleOptions encryptionModuleOptions,
            IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory)
            : base(encryptionOptions, symmetricAlgorithmFactory)
        {
            this.encryptionModuleOptions = encryptionModuleOptions;
        }

        public string Encrypt(string value, string key)
        {
            return Encrypt(value, EncryptionOptions.UseKey(key));
        }

        public string Encrypt(string value, IEncryptionOptions encryptionOptions)
        {
            MemoryStream EncryptOperation(MemoryStream memoryStream, CryptoStream cryptoStream)
            {
                using (var streamWriter = new StreamWriter(cryptoStream, encryptionOptions.Encoding))
                    streamWriter.Write(value);
                return memoryStream;
            }

            value = PerformEncryptionCaseConventionOperation(value, encryptionModuleOptions.EncryptionCaseConvention);

            var encryptedBytes = ExecuteSymmetricOperation(Shared.Enumerations.EncryptionMode.Encrypt,
                encryptionOptions, CryptoStreamMode.Write, EncryptOperation).ToArray();


            return Convert.ToBase64String(encryptedBytes);
        }
    }
}
