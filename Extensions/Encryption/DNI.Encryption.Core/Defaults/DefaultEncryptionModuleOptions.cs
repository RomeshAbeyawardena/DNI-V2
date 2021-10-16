using DNI.Encryption.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Core.Defaults
{
    public class DefaultEncryptionModuleOptions : IEncryptionModuleOptions
    {
        public DefaultEncryptionModuleOptions()
        {
            EncryptionOptions = new Dictionary<string, IEncryptionOptions>();
        }

        public IDictionary<string, Func<IServiceProvider, IEncryptionOptions>> EncryptionOptionsFactory { get; }

        public IDictionary<string, IEncryptionOptions> EncryptionOptions { get; }

        public bool UseModuleAssemblies { get; set; }
    }
}
