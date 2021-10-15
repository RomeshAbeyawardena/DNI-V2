using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static async Task<IModuleStartup> StartModules(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default)
        {
            var moduleStartup = serviceProvider.GetRequiredService<IModuleStartup>();

            await moduleStartup.StartAsync(cancellationToken);

            return moduleStartup;
        }
    }
}
