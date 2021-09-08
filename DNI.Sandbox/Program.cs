using DNI.Extensions;
using DNI.ModuleLoader.Core.Bootstrapper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace DNI.Sandbox
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var services = new ServiceCollection();

            services
                .AddLogging(opt => opt.AddConsole())
                .RegisterAppModuleLoader<SandboxAppModuleLoader>();
            AppModuleBootstapper.Bootstrap<SandboxAppModuleLoader>(services).Load("modules.json");
        }
    }
}
