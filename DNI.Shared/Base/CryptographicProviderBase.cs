using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
