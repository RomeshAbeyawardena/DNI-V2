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
        private DefaultEncryptionModuleOptions encryptionModuleOptions = new();

        public IEncryptionModuleOptions Build()
        {
            return encryptionModuleOptions;
        }

        public IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure)
        {
            configure?.Invoke(encryptionModuleOptions);
            return this;
        }

        public IEncryptionOptionsBuilder UseModuleAssemblies()
        {
            encryptionModuleOptions.UseModuleAssemblies = true;
            return this;
        }
    }
}
