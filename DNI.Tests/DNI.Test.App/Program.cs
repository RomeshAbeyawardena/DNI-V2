using DNI.Core.Defaults.Hosts;
using DNI.Data.Extensions;
using DNI.Data.Shared.Base;
using DNI.Extensions;
using DNI.Mediator.Extensions;
using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Shared.Abstractions.Hosts;
using DNI.Shared.Test;
using DNI.Web.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    class MyDbContext : DbContextBase
    {
        public MyDbContext(DbContextOptions dbContextOptions) 
            : base(dbContextOptions)
        {
        }

        public DbSet<User> Users { get; set; }
    }


    class Program
    {
        static IConsoleHost consoleHost;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.CancelKeyPress += Console_CancelKeyPress;
            consoleHost = ConsoleHost.Build(ConfigureConsoleHost);
               
            await consoleHost.StartAsync();
        }

        public static void ConfigureConsoleHost(IConsoleHost consoleHost)
        {
            consoleHost
                .ConfigureServices<Startup>(ConfigureServices)
                .AddConfiguration(c => c
                    .AddInMemoryCollection()
                    .AddJsonFile("appsettings.json")
                    .AddUserSecrets(typeof(Program).Assembly, false));
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddLogging(c => c.AddConsole())
                .ConfigureMigrationManagerModuleConfiguration(c => c.AddMigration("Default", DefaultMigration))
                .ConfigureDbContextModule(c => c.AddDbContext<MyDbContext>((s, b) => b
                    .UseSqlServer(s.GetRequiredService<IConfiguration>()
                        .GetConnectionString("default"))
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution), ServiceLifetime.Scoped))
                .ConfigureMediatorModule(a => a.AddModuleAssemblies())
                .ConfigureWebModule(a => a.UseModuleAssemblies()
                .ConfigureWebHost(ConfigureWebHost))
                .RegisterModules(build => build
                    .ConfigureAssemblies(c => c
                    .AddAssembly(MigrationManager.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })
                    .AddAssembly(Data.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })
                    .AddAssembly(Mediator.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })
                    .AddAssembly(Web.Modules.This.Assembly, a => { a.OnStartup = true; a.Discoverable = true; })));
        }

        private static void ConfigureWebHost(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.Configure(ConfigureApp);
        }

        private static void ConfigureApp(IApplicationBuilder applicationBuilder)
        {
            applicationBuilder
                .UseRouting()
                .UseEndpoints(c => c.MapControllers());
        }

        private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
        {
            e.Cancel = true;
            //consoleHost.Stop();
            consoleHost.Dispose();
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
