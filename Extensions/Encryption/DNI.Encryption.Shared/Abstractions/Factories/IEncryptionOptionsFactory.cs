namespace DNI.Encryption.Shared.Abstractions.Factories
{
    public interface IEncryptionOptionsFactory
    {
        IEncryptionOptions GetEncryptionOptions(string sectionName);
    }
}
