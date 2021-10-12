using System.Security.Cryptography;

namespace DNI.Shared.Base
{
    public interface ISymmetricAlgorithmFactory
    {
        SymmetricAlgorithm GetSymmetricAlgorithm(Shared.Enumerations.SymmetricAlgorithm symmetricAlgorithm);
    }
}
