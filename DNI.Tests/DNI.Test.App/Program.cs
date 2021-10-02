using DNI.Core.Defaults.Hosts;
using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var s = ConsoleHost.Build(h => h
                .ConfigureServices<Startup>(s => s
                .AddMigrationServices()
                .AddMigration("Default", Configure)
                .RegisterModules(build => build.ConfigureAssemblies(c => c.AddAssembly(MigrationManager.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })))));

            await s.StartAsync();
        }

        private static IMigrationOptions Configure(IServiceProvider arg1, IMigrationConfigurator arg2)
        {
            throw new NotImplementedException();
        }
    }
}
