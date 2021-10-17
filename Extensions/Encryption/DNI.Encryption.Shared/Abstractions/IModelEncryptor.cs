namespace DNI.Encryption.Shared.Abstractions
{
    public interface IModelEncryptor
    {
        T Encrypt<T>(T model);
        T Decrypt<T>(T model);
    }
}
