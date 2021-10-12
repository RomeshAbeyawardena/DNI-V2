namespace DNI.Shared.Abstractions
{
    public interface IDecryptor : ICryptographicProvider
    {
        string Decrypt(string value);
    }
}
