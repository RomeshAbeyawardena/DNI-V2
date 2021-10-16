using DNI.Encryption.Shared.Abstractions;
using DNI.Encryption.Shared.Abstractions.Factories;
using DNI.Extensions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults.Factories
{
    [RegisterService]
    public class DefaultEncryptionOptionsFactory : IEncryptionOptionsFactory
    {
        private readonly IConfiguration configuration;
        private readonly IDictionary<string, IEncryptionOptions> encryptionOptions;

        public DefaultEncryptionOptionsFactory(
            IConfiguration configuration,
            IDictionary<string, IEncryptionOptions> encryptionOptions)
        {
            this.configuration = configuration;
            this.encryptionOptions = encryptionOptions;
        }

        public IEncryptionOptions GetEncryptionOptions(string sectionName, string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                var paths = path.Split("//", StringSplitOptions.RemoveEmptyEntries);
                IConfigurationSection section = configuration.GetSection(paths.FirstOrDefault());

                if(section == null)
                {
                    throw new NullReferenceException();
                }

                foreach(var currentSectionName in paths.RemoveAt(0))
                {
                    section = configuration.GetSection(currentSectionName);
                }

                return new DefaultEncryptionOptions(section);
            }

            return new DefaultEncryptionOptions(encryptionOptions, sectionName);

        }
    }
}
