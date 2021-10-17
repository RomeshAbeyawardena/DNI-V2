using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

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

        public static IServiceCollection Merge(this IServiceCollection target, IServiceCollection services)
        {
            foreach (var service in services)
            {
                if (!target.Contains(service))
                {
                    target.Add(service);
                }
            }

            return target;
        }

    }
}
