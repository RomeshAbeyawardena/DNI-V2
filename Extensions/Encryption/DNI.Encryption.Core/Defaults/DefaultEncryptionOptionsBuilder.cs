using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Core.Defaults
{
    public class DefaultEncryptionOptionsBuilder : IEncryptionOptionsBuilder
    {
        public IEncryptionOptions Build()
        {
            throw new NotImplementedException();
        }

        public IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure)
        {
            throw new NotImplementedException();
        }

        public IEncryptionOptionsBuilder UseModuleAssemblies()
        {
            throw new NotImplementedException();
        }
    }
}
