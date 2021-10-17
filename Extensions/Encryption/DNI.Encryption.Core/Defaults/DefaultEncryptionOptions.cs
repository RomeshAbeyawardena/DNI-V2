using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace DNI.Core.Defaults
{
    public class DefaultEncryptionOptions : IEncryptionOptions
    {
        public DefaultEncryptionOptions(IEncryptionOptionsConfiguration encryptionOptionsConfiguration)
        {
            Key = encryptionOptionsConfiguration.Key;
            InitialVector = encryptionOptionsConfiguration.InitialVector;

            if (!string.IsNullOrWhiteSpace(encryptionOptionsConfiguration.SymmetricAlgorithm))
                Algorithm = Enum.Parse<SymmetricAlgorithm>(encryptionOptionsConfiguration.SymmetricAlgorithm);

            if (!string.IsNullOrWhiteSpace(encryptionOptionsConfiguration.HashAlgorithm))
                HashAlgorithm = Enum.Parse<HashAlgorithm>(encryptionOptionsConfiguration.HashAlgorithm);

            if (!string.IsNullOrWhiteSpace(encryptionOptionsConfiguration.Encoding))
                Encoding = Encoding.GetEncoding(encryptionOptionsConfiguration.Encoding);
        }

        public DefaultEncryptionOptions(string key, string initialVector, SymmetricAlgorithm algorithm, Encoding encoding)
        {
            Key = key;
            InitialVector = initialVector;
            Algorithm = algorithm;
            Encoding = encoding;
        }

        public DefaultEncryptionOptions(IDictionary<string, IEncryptionOptions> encryptionOptionsDict, string key)
        {
            if (encryptionOptionsDict.TryGetValue(key, out var encryptionOptions))
            {
                Encoding = encryptionOptions.Encoding;
                Algorithm = encryptionOptions.Algorithm;
                Key = encryptionOptions.Key;
                InitialVector = encryptionOptions.InitialVector;
            }
        }

        public Encoding Encoding { get; set; }

        public SymmetricAlgorithm? Algorithm { get; set; }

        public HashAlgorithm? HashAlgorithm { get; set; }

        public string Key { get; set; }

        public string InitialVector { get; set; }

        public IEncryptionOptions UseKey(string key)
        {
            var encryptionOptions = this.MemberwiseClone() as DefaultEncryptionOptions;
            encryptionOptions.Key = key;
            return encryptionOptions;
        }
    }
}
