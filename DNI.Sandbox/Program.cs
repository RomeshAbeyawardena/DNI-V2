using DNI.Extensions;
using DNI.ModuleLoader.Core.Bootstrapper;
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
                
            await Task.WhenAll(s.Load("modules.json"));
            
        }
    }
}
