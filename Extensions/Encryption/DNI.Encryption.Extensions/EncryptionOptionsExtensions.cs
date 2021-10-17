using DNI.Core.Defaults;
using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Enumerations;
using System.Text;

namespace DNI.Encryption.Extensions
{
    public static class EncryptionOptionsExtensions
    {
        public static IEncryptionModuleOptions AddEncryptionOption(this IEncryptionModuleOptions options, string key, string initialVector, SymmetricAlgorithm algorithm, Encoding encoding)
        {
            return options.AddEncryptionOption(string.Empty, key, initialVector, algorithm, encoding);
        }

        public static IEncryptionModuleOptions AddEncryptionOption(this IEncryptionModuleOptions options, string keyName, string key, string initialVector, SymmetricAlgorithm algorithm, Encoding encoding)
        {
            options.EncryptionOptions.Add(keyName, new DefaultEncryptionOptions(key, initialVector, algorithm, encoding));
            return options;
        }

        public static IEncryptionModuleOptions AddEncryptionOption(this IEncryptionModuleOptions options, string keyName, IEncryptionOptionsConfiguration encryptionOptionsConfiguration)
        {
            options.EncryptionOptions.Add(keyName, new DefaultEncryptionOptions(encryptionOptionsConfiguration));
            return options;
        }

        public static IEncryptionModuleOptions ImportConfiguration(this IEncryptionModuleOptions options, string path = default)
        {
            options.ImportConfiguration = true;
            options.ImportConfigurationPath = path;
            return options;
        }
    }
}
