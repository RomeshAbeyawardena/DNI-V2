using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Encryption.Shared.Enumerations;
using DNI.Shared.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultHasherAlgorithmFactory : IHasherAlgorithmFactory
    {
        private readonly IDictionary<HashAlgorithm, Func<System.Security.Cryptography.HashAlgorithm>> algorithms;

        public DefaultHasherAlgorithmFactory(IDictionary<HashAlgorithm, Func<System.Security.Cryptography.HashAlgorithm>> algorithms)
        {
            this.algorithms = algorithms;
        }

        public System.Security.Cryptography.HashAlgorithm GetHashAlgorithm(HashAlgorithm hashAlgorithm)
        {
            if (algorithms.TryGetValue(hashAlgorithm, out var algorithmFactory))
                return algorithmFactory();

            return null;
        }
    }
}
