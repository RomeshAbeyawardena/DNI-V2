namespace DNI.Encryption.Shared.Abstractions
{
    public interface IDecryptor : ICryptographicProvider
    {
        string Decrypt(string value, string key);
        string Decrypt(string value, IEncryptionOptions encryptionOptions);
    }
}
