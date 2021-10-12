using DNI.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IEncryptionOptions
    {
        Encoding Encoding { get; }
        SymmetricAlgorithm Algorithm { get; }
    }
}
