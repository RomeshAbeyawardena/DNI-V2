using System;

namespace DNI.Encryption.Shared.Abstractions.Builders
{
    public interface IEncryptionOptionsBuilder
    {
        IEncryptionModuleOptions Build();
        IEncryptionOptionsBuilder ConfigureOptions(Action<IEncryptionModuleOptions> configure);
        IEncryptionOptionsBuilder ImportConfiguration(string path = default);
    }
}
