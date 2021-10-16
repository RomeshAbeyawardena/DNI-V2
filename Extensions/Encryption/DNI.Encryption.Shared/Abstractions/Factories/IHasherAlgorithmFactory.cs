using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions.Factories
{
    public interface IHasherAlgorithmFactory
    {
        HashAlgorithm GetHashAlgorithm(Enumerations.HashAlgorithm hashAlgorithm);
    }
}
