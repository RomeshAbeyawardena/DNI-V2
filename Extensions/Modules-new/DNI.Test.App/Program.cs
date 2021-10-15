using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class Program
    {
        static Task Main(string[] args)
        {
            ServiceCollection services = new();

             services
                .AddModules(configure => configure.AddModule(typeof(MyModule)).AddModule(typeof(MyModule2)));

            var serviceProvider = services.BuildServiceProvider();

            using var moduleStartup = serviceProvider.GetRequiredService<IModuleStartup>();

            Console.WriteLine("Hello World!");
            return moduleStartup
                .StartAsync(CancellationToken.None);
            
        }
    }
}
