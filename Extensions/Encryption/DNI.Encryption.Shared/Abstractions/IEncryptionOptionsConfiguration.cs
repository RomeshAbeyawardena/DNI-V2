namespace DNI.Encryption.Shared.Abstractions
{
    public interface IEncryptionOptionsConfiguration
    {
        string Key { get; set; }
        string InitialVector { get; set; }
        string Encoding { get; set; }
        string SymmetricAlgorithm { get; set; }
        string HashAlgorithm { get; set; }
        IEncryptionOptions Build();
    }
}
