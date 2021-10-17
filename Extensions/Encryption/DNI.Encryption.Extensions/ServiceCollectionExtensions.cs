using DNI.Core.Defaults.Builders;
using DNI.Encryption.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DNI.Encryption.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
        {
            return services
                .AddSingleton(Configure())
                .AddSingleton(ConfigureHash());
        }

        public static IServiceCollection ConfigureEncryptionOptions(this IServiceCollection services,
            Func<IServiceProvider, IEncryptionOptions> configureEncryptionOptions)
        {
            return services.AddSingleton(configureEncryptionOptions);
        }

        private static IDictionary<Shared.Enumerations.HashAlgorithm, Func<HashAlgorithm>> ConfigureHash()
        {
            return DictionaryBuilder.Build<Shared.Enumerations.HashAlgorithm, Func<HashAlgorithm>>(db => db
            .Add(Shared.Enumerations.HashAlgorithm.HMACMD5, () => HashAlgorithm.Create("MD5"))
            .Add(Shared.Enumerations.HashAlgorithm.HMACSHA1, () => HashAlgorithm.Create("SHA1"))
            .Add(Shared.Enumerations.HashAlgorithm.HMACSHA256, () => HashAlgorithm.Create("SHA256"))
            .Add(Shared.Enumerations.HashAlgorithm.HMACSHA384, () => HashAlgorithm.Create("SHA384"))
            .Add(Shared.Enumerations.HashAlgorithm.HMACSHA512, () => HashAlgorithm.Create("SHA512")));
        }

        private static IDictionary<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>> Configure()
        {
            return DictionaryBuilder.Build<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>>(db => db
                .Add(Shared.Enumerations.SymmetricAlgorithm.Aes, () => SymmetricAlgorithm.Create("AES"))
                .Add(Shared.Enumerations.SymmetricAlgorithm.RSA, () => SymmetricAlgorithm.Create("RSA")));
        }
    }
}
