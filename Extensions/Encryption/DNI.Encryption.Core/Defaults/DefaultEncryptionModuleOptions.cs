using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Enumerations;
using System;
using System.Collections.Generic;

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

        public bool ImportConfiguration { get; set; }
        public string ImportConfigurationPath { get; set; }
        public EncryptionCaseConvention EncryptionCaseConvention { get; set; }
    }
}
