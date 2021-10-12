using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public interface ISymmetricAlgorithmFactory
    {
        SymmetricAlgorithm GetSymmetricAlgorithm(Shared.Enumerations.SymmetricAlgorithm symmetricAlgorithm);
    }
}
