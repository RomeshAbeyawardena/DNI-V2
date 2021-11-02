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

            Console.WriteLine("Using: {0}", encryptionOptions);

            using (var hashAlgorithm = hasherAlogrithmFactory.GetHashAlgorithm(encryptionOptions.HashAlgorithm.HasValue
                ? encryptionOptions.HashAlgorithm.Value
                : Shared.Enumerations.HashAlgorithm.HMACSHA512))
                return Convert.ToBase64String(hashAlgorithm.ComputeHash(encryptionOptions.Encoding.GetBytes(value)));
        }
    }
}
