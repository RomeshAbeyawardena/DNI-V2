using DNI.Encryption.Shared.Enumerations;
using System.Text;

namespace DNI.Encryption.Shared.Abstractions
{
    public interface IEncryptionOptions
    {
        Encoding Encoding { get; }
        SymmetricAlgorithm Algorithm { get; }
        string Key { get; }
        string InitialVector { get; }

        IEncryptionOptions UseKey(string key);
    }
}
