using DNI.Modules.Shared.Builders;
using System;

namespace DNI.Encryption.Shared.Abstractions.Builders
{
    public interface IEncryptionOptionsBuilder : IModuleOptionsBuilder<IEncryptionModuleOptions>
    {
        IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure);
        IEncryptionOptionsBuilder ImportConfiguration(string path = default);
    }
}
