using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Web;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            ServiceCollection services = new();

             services
                .AddModules(configure => configure
                .AddWebModule()
                .AddModule(typeof(MyModule))
                .AddModule(typeof(MyModule2)));

            var serviceProvider = services.BuildServiceProvider();

            using var moduleStartup = await serviceProvider
                .StartModules(CancellationToken.None);

            await moduleStartup.StopAsync(CancellationToken.None);
        }
    }
}
