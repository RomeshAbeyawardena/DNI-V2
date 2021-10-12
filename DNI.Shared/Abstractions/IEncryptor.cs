namespace DNI.Shared.Abstractions
{
    public interface IEncryptor : ICryptographicProvider
    {
        string Encrypt(string value);
    }
}
