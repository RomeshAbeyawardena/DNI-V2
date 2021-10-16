﻿using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Encryption.Shared.Base;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Encryption.Core.Defaults
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

            return ExecuteSymmetricOperation(Shared.Enumerations.EncryptionMode.Decrypt, 
                encryptionOptions, CryptoStreamMode.Read, (m, c) =>
                {
                    using (var streamReader = new StreamReader(c, encryptionOptions.Encoding))
                    {
                        return streamReader.ReadToEnd();
                    }
                }, encryptedBytes);
        }
    }
}
