namespace DNI.Encryption.Shared.Abstractions
{
    public interface ICryptographicProvider
    {
        IEncryptionOptions EncryptionOptions { get; }
    }
}
