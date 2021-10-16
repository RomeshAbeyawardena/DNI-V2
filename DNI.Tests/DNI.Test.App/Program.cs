using DNI.Core.Defaults.Hosts;
using DNI.Data.Extensions;
using DNI.Data.Modules;
using DNI.Data.Shared.Base;
using DNI.Encryption.Extensions;
using DNI.Encryption.Modules.Extensions;
using DNI.Extensions;
using DNI.Mediator.Modules.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Shared.Abstractions.Hosts;
using DNI.Shared.Test;
using DNI.Web.Modules.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Reflection;
using System.Text;
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
                .AddModules(builder => builder
                .ConfigureDbContextModule(builder => builder
                    .AddDbContext<MyDbContext>((s, b) => b.UseSqlServer(s
                        .GetService<IConfiguration>()
                        .GetConnectionString("default")), ServiceLifetime.Scoped))
                .ConfigureMediatorModule(builder => builder.AddModuleAssemblies())
                .ConfigureWebModule(builder => builder.AddModuleAssemblies())
                .ConfigureEncryptionModule(builder => builder
                    .UseModuleAssemblies()
                    .ConfigureOptions(s => { s
                        .AddEncryptionOption("NWU1Nzc0ZTZkYjJlNDI3ZmI5MzVkZmZiYWJkODJlZjA=", "NTEwMWQ3OGFiNWEwNDcx", Encryption.Shared.Enumerations.SymmetricAlgorithm.Aes, Encoding.Default)
                        .AddEncryptionOption("Personal", "YjU5ZDQzNjM5YzliNDA5ZTlkM2MxNGRlNTgyNGFjNjg=", "YmQ1MzkyMWRjOWQ4YTU3ZA==", Encryption.Shared.Enumerations.SymmetricAlgorithm.Aes, Encoding.Default)
                        .AddEncryptionOption("Common", "ZDhhMjliZmMxNDZjNDhhNGIwODBiMTFhNjI1ZmFhYjk=", "YmQ1MzkyMWRjOWQ4YTU3ZA==", Encryption.Shared.Enumerations.SymmetricAlgorithm.Aes, Encoding.Default)
                        .AddEncryptionOption("Credential", "NjhlMzc2MGE5ODYzNDhiOTkyZDIwNWYxMTk0M2RhM2E=", "OTcyYTU0OTUxZjdhMzE0MA==", Encryption.Shared.Enumerations.SymmetricAlgorithm.Aes, Encoding.Default)
                        .AddEncryptionOption("Secondary", "NDI3NzAyMjk4NTI2NDdhMzlmMmNiZDBmNWM1NjRhZWQ=", "MDRiM2FmNTcwOWVjNDY2MA==", Encryption.Shared.Enumerations.SymmetricAlgorithm.Aes, Encoding.Default); 
                    })));
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
