using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Shared.Test;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Modules
{
    [RequiresDependencies(typeof(Core.This))]
    public class MigrationManagerModule : ModuleBase
    {
        private readonly IMigrationQueryBuilder migrationQueryBuilder;

        [Resolve] private IRepository<User> UserRepository { get; set; }

        [Resolve] private ILogger<MigrationManagerModule> Logger { get; set; }

        [Resolve] private static IMigrationManagerModuleConfiguration Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMigrationServices();

            foreach (var (k, v) in Configuration)
            {
                services.AddMigration(k, v);
            }

        }

        public MigrationManagerModule(IMigrationQueryBuilder migrationQueryBuilder)
        {
            this.migrationQueryBuilder = migrationQueryBuilder;
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Migration manager running on {0}", Thread.CurrentThread.ManagedThreadId);
            var sql = migrationQueryBuilder.BuildMigrations("sql");

            Logger.LogInformation(sql);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            Logger.LogInformation("Migration manager stopping");
            return Task.CompletedTask;
        }
    }
}
