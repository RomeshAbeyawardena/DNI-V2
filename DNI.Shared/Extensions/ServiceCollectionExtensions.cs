using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DNI.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Prune(this IServiceCollection services)
        {
            var distinctServices = services.Distinct().ToArray();

            if(distinctServices.Length < services.Count)
            {
                services.Clear();

                foreach (var service in distinctServices)
                {
                    services.Add(service);
                }

            }

            return services;
        }
    }
}
