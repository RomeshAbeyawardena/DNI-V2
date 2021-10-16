using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions.Factories
{
    public interface IEncryptionOptionsFactory
    {
        IEncryptionOptions GetEncryptionOptions(string sectionName, string path = default);
    }
}
