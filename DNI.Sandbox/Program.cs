using DNI.Extensions;
using DNI.ModuleLoader.Core;
using DNI.ModuleLoader.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var services = new ServiceCollection();

            services
                .AddLogging(opt => opt.AddConsole())
                .RegisterAppModuleLoader<SandboxAppModuleLoader>();
            var s = AppModuleBootstapper.Bootstrap<SandboxAppModuleLoader>(services);
            var modules = s.Load("modules.json");
            
            await Task.WhenAll(s.RunAsync());
            
        }
    }
}
