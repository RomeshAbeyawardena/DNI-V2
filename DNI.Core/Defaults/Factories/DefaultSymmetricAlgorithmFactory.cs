using DNI.Shared.Attributes;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultSymmetricAlgorithmFactory : ISymmetricAlgorithmFactory
    {
        private readonly IDictionary<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>> symmetricAlgorithmDictionaryFactory;

        public DefaultSymmetricAlgorithmFactory(IDictionary<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>> symmetricAlgorithmDictionaryFactory)
        {
            this.symmetricAlgorithmDictionaryFactory = symmetricAlgorithmDictionaryFactory;
        }

        public SymmetricAlgorithm GetSymmetricAlgorithm(Shared.Enumerations.SymmetricAlgorithm symmetricAlgorithm)
        {
            if (symmetricAlgorithmDictionaryFactory.TryGetValue(symmetricAlgorithm, out var algorithmFactory))
            {
                return algorithmFactory();
            }

            return null;
        }
    }
}
