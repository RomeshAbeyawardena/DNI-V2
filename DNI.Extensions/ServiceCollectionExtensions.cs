using DNI.Core.Defaults.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;

namespace DNI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection OutputServices(this IServiceCollection services)
        {
            foreach (var service in services)
            {
                Debug.WriteLine("{0} {1} {1} {2} {3}", service.ServiceType, service?.ImplementationType, service?.ImplementationFactory, service?.ImplementationInstance);
            }

            return services;
        }

        public static IServiceCollection AddEncryptionServices(this IServiceCollection services)
        {
            return services.AddSingleton(Configure());
        }

        private static IDictionary<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>> Configure()
        {
            return DictionaryBuilder.Build<Shared.Enumerations.SymmetricAlgorithm, Func<SymmetricAlgorithm>>(db => db
                .Add(Shared.Enumerations.SymmetricAlgorithm.Aes, () => SymmetricAlgorithm.Create("AES"))
                .Add(Shared.Enumerations.SymmetricAlgorithm.RSA, () => SymmetricAlgorithm.Create("RSA")));
        }

    }
}
