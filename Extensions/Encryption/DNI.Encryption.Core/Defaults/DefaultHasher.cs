using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using System;

namespace DNI.Encryption.Core.Defaults
{
    [RegisterService]
    public class DefaultHasher : IHasher
    {
        private readonly IHasherAlgorithmFactory hasherAlogrithmFactory;
        private readonly IEncryptionOptions encryptionOptions;

        public DefaultHasher(IHasherAlgorithmFactory hasherAlgorithmFactory,
            IEncryptionOptions encryptionOptions)
        {
            this.hasherAlogrithmFactory = hasherAlgorithmFactory;
            this.encryptionOptions = encryptionOptions;
        }

        public string HashString(string value, IEncryptionOptions encryptionOptions)
        {
            if (encryptionOptions == null)
                encryptionOptions = this.encryptionOptions;

            using (var hashAlgorithm = hasherAlogrithmFactory.GetHashAlgorithm(encryptionOptions.HashAlgorithm.Value))
                return Convert.ToBase64String(hashAlgorithm.ComputeHash(encryptionOptions.Encoding.GetBytes(value)));
        }
    }
}
