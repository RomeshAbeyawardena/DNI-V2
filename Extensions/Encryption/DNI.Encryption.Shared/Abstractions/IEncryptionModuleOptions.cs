using System;
using System.Collections.Generic;

namespace DNI.Encryption.Shared.Abstractions
{
    public interface IEncryptionModuleOptions
    {
        IDictionary<string, Func<IServiceProvider, IEncryptionOptions>> EncryptionOptionsFactory { get; }
        IDictionary<string, IEncryptionOptions> EncryptionOptions { get; }
        bool ImportConfiguration { get; set; }
        string ImportConfigurationPath { get; set; }
    }
}
