namespace DNI.Encryption.Shared.Abstractions
{
    public interface IHasher
    {
        string HashString(string value, IEncryptionOptions encryptionOptions);
    }
}
