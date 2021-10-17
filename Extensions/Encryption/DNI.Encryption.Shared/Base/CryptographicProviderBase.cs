using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Enumerations;
using System;
using System.IO;
using System.Security.Cryptography;

namespace DNI.Encryption.Shared.Base
{
    public abstract class CryptographicProviderBase : ICryptographicProvider
    {
        private readonly ISymmetricAlgorithmFactory SymmetricAlgorithmFactory;

        protected System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm(Enumerations.SymmetricAlgorithm symmetricAlgorithm)
        {
            return SymmetricAlgorithmFactory.GetSymmetricAlgorithm(symmetricAlgorithm);
        }

        protected T ExecuteSymmetricOperation<T>(
            EncryptionMode encryptionMode,
            IEncryptionOptions encryptionOptions,
            CryptoStreamMode cryptoStreamMode,
            Func<MemoryStream, CryptoStream, T> operation,
            byte[] memoryStreamBuffer = null)
        {
            ICryptoTransform GetCryptoTransform(System.Security.Cryptography.SymmetricAlgorithm symmetricAlgorithm)
            {
                var key = Convert.FromBase64String(encryptionOptions.Key);
                var iv = Convert.FromBase64String(encryptionOptions.InitialVector);

                if (key.Length > 32)
                    throw new InvalidOperationException();

                if (iv.Length > 16)
                    throw new InvalidOperationException();

                switch (encryptionMode)
                {

                    case EncryptionMode.Encrypt:
                        return symmetricAlgorithm.CreateEncryptor(key, iv);
                    case EncryptionMode.Decrypt:
                        return symmetricAlgorithm.CreateDecryptor(key, iv);
                    default:
                        throw new NotSupportedException();
                }
            }


            using (var memoryStream = memoryStreamBuffer == null 
                ? new MemoryStream()
                : new MemoryStream(memoryStreamBuffer))
            {
                using (var algorithm = GetSymmetricAlgorithm(encryptionOptions.Algorithm.Value))
                using (var encryptor = GetCryptoTransform(algorithm))
                using (var cryptoStream = new CryptoStream(memoryStream, encryptor, cryptoStreamMode))
                    return operation(memoryStream, cryptoStream);
            }
        }

        public CryptographicProviderBase(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory)
        {
            EncryptionOptions = encryptionOptions;
            SymmetricAlgorithmFactory = symmetricAlgorithmFactory;
        }

        public IEncryptionOptions EncryptionOptions { get; }
    }
}
