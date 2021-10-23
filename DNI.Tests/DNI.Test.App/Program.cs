using DNI.Core.Defaults.Hosts;
using DNI.Modules.Extensions;
using DNI.Test.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public static class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            using var consoleHost = ConsoleHost
                .Build(build => build.ConfigureServices<Startup>(ConfigureServices));

            await consoleHost.StartAsync(args: args);
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(c => c.AddConsole())
                .AddModules(builder => builder
                    .AddModule<MyDbModule>()
                    .AddModule<MyWebModule>());
        }
    }
}
