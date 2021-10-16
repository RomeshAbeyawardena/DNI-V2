using DNI.Core.Defaults.Builders;
using DNI.Encryption.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Encryption.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
        {
            return services.AddSingleton(Configure());
        }

        public static IServiceCollection ConfigureEncryptionOptions(this IServiceCollection services,
            Func<IServiceProvider, IEncryptionOptions> configureEncryptionOptions)
        {
            return services.AddSingleton(configureEncryptionOptions);
        }

        private static IDictionary<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>> Configure()
        {
            return DictionaryBuilder.Build<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>>(db => db
                .Add(Shared.Enumerations.SymmetricAlgorithm.Aes, () => SymmetricAlgorithm.Create("AES"))
                .Add(Shared.Enumerations.SymmetricAlgorithm.RSA, () => SymmetricAlgorithm.Create("RSA")));
        }
    }
}
