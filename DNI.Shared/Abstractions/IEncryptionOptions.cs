﻿using DNI.Shared.Enumerations;
using System.Text;

namespace DNI.Shared.Abstractions
{
    public interface IEncryptionOptions
    {
        Encoding Encoding { get; }
        SymmetricAlgorithm Algorithm { get; }
    }
}
