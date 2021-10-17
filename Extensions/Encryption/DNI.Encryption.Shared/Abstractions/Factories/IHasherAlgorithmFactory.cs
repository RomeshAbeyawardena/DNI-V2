using System.Security.Cryptography;

namespace DNI.Encryption.Shared.Abstractions.Factories
{
    public interface IHasherAlgorithmFactory
    {
        HashAlgorithm GetHashAlgorithm(Enumerations.HashAlgorithm hashAlgorithm);
    }
}
