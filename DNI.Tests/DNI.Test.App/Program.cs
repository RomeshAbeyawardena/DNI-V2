using DNI.Core.Defaults.Hosts;
using DNI.Data.Extensions;
using DNI.Extensions;
using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class MyDbContext
    {

    }

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            using var s = ConsoleHost.Build(h => h
                .Configure(c => c
                    .AddInMemoryCollection()
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets(typeof(Program).Assembly, false))
                .ConfigureServices<Startup>(s => s
                .ConfigureMigrationManagerModuleConfiguration(c => c.AddMigration("Default", DefaultMigration))
                .RegisterModules(build => build
                    .ConfigureAssemblies(c => c
                    .AddAssembly(MigrationManager.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })
                    .AddAssembly(Data.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })))
                .ConfigureDbContextModule(c => c.AddDbContext<MyDbContext>(b => b.UseQueryTrackingBehavior(Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTrackingWithIdentityResolution), ServiceLifetime.Scoped))
                .OutputServices()));

            await s.StartAsync();
        }

        private static IDbConnection ConfigureDbConnection(IServiceProvider serviceProvider)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            return serviceProvider.GetRequiredService<IDbConnectionFactory>()
                .GetDbConnection(configuration.GetConnectionString("default"));
        }

        private static IMigrationOptions DefaultMigration(IServiceProvider arg1, IMigrationConfigurator migrationConfigurator)
        {
            return migrationConfigurator
                .Configure(b => b.AddAssembly(Assembly.GetExecutingAssembly())
                .ConfigureDbConnectionFactory(ConfigureDbConnection))
                .Build();
        }
    }
}
