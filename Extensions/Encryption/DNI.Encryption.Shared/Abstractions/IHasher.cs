using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions
{
    public interface IHasher
    {
        string HashString(string value, IEncryptionOptions encryptionOptions);
    }
}
