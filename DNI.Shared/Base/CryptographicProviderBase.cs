using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;

namespace DNI.Shared.Base
{
    public abstract class CryptographicProviderBase : ICryptographicProvider
    {
        private readonly ISymmetricAlgorithmFactory SymmetricAlgorithmFactory;

        protected System.Security.Cryptography.SymmetricAlgorithm GetSymmetricAlgorithm(SymmetricAlgorithm symmetricAlgorithm)
        {
            return SymmetricAlgorithmFactory.GetSymmetricAlgorithm(symmetricAlgorithm);
        }

        public CryptographicProviderBase(IEncryptionOptions encryptionOptions, ISymmetricAlgorithmFactory symmetricAlgorithmFactory)
        {
            EncryptionOptions = encryptionOptions;
            SymmetricAlgorithmFactory = symmetricAlgorithmFactory;
        }

        public IEncryptionOptions EncryptionOptions { get; }
    }
}
