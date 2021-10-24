using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Modules
{
    public class MigrationManagerModule : ModuleBase
    {
        private readonly IMigrationQueryBuilder migrationQueryBuilder;

        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            services
                .AddMigrationServices();

            foreach (var (k, v) in moduleConfiguration.GetOptions<IMigrationManagerModuleConfiguration>(ModuleDescriptor))
            {
                services.AddMigration(k, v);
            }

        }

        public MigrationManagerModule(IMigrationQueryBuilder migrationQueryBuilder)
        {
            this.migrationQueryBuilder = migrationQueryBuilder;
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            Console.WriteLine("Migration manager running on {0}", Thread.CurrentThread.ManagedThreadId);
            var sql = migrationQueryBuilder.BuildMigrations("sql");

            Console.WriteLine(sql);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            Console.WriteLine("Migration manager stopping");
            return Task.CompletedTask;
        }

        public override void OnDispose(bool disposing)
        {
            throw new NotImplementedException();
        }
    }
}
