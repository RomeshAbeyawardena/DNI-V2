using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Shared.Abstractions
{
    public interface IEncryptionModuleOptions
    {
        IDictionary<string, Func<IServiceProvider, IEncryptionOptions>> EncryptionOptionsFactory { get; }
        IDictionary<string, IEncryptionOptions> EncryptionOptions { get; }
        bool ImportConfiguration { get; }
        string ImportConfigurationPath { get; }
    }
}
