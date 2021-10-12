namespace DNI.Shared.Abstractions
{
    public interface IEncryptor : ICryptographicProvider
    {
        string Encrypt(string value, string key);
        string Encrypt(string value, IEncryptionOptions encryptionOptions);
    }
}
