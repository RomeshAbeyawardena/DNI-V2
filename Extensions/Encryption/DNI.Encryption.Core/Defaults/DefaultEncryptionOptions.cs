using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Enumerations;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults
{
    public class DefaultEncryptionOptions : IEncryptionOptions
    {
        
        public DefaultEncryptionOptions(string key, string initialVector, SymmetricAlgorithm algorithm, Encoding encoding)
        {
            Key = key;
            InitialVector = initialVector;
            Algorithm = algorithm;
            Encoding = encoding;
        }

        public DefaultEncryptionOptions(IConfiguration configuration, string sectionName)
        {
            configuration.GetSection(sectionName).Bind(this);
        }

        public DefaultEncryptionOptions(IDictionary<string, IEncryptionOptions> encryptionOptionsDict, string key)
        {
            if(encryptionOptionsDict.TryGetValue(key, out var encryptionOptions))
            {
                Encoding = encryptionOptions.Encoding;
                Algorithm = encryptionOptions.Algorithm;
                Key = encryptionOptions.Key;
                InitialVector = encryptionOptions.InitialVector;
            }
        }

        public Encoding Encoding { get; set; }

        public SymmetricAlgorithm Algorithm { get; set; }

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
