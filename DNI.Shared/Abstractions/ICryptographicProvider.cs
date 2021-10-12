namespace DNI.Shared.Abstractions
{
    public interface ICryptographicProvider
    {
        IEncryptionOptions EncryptionOptions { get; }
    }
}
