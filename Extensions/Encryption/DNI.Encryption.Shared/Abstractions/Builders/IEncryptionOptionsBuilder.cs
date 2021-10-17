using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions.Builders
{
    public interface IEncryptionOptionsBuilder
    {
        IEncryptionModuleOptions Build();
        IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure);
        IEncryptionOptionsBuilder ImportConfiguration(string path = default);
    }
}
