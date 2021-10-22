using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection Prune(this IServiceCollection services)
        {
            var distinctServices = services.Distinct().ToArray();

            services.Clear();

            foreach (var service in distinctServices)
            {
                services.Add(service);
            }

            return services;
        }
    }
}
