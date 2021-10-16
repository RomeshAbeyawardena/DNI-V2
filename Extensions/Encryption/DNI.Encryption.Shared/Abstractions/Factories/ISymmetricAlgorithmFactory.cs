using System.Security.Cryptography;

namespace DNI.Encryption.Shared.Base
{
    public interface ISymmetricAlgorithmFactory
    {
        SymmetricAlgorithm GetSymmetricAlgorithm(Shared.Enumerations.SymmetricAlgorithm symmetricAlgorithm);
    }
}
