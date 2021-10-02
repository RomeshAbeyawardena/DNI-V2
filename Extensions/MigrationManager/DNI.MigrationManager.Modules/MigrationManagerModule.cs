using DNI.MigrationManager.Extensions;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.MigrationManager.Core.Modules
{
    public class MigrationManagerModule : ModuleBase
    {
        [Resolve]
        private static IMigrationManagerModuleConfiguration Configuration { get; set; }

        private readonly IMigrationManagerModuleConfiguration configuration;

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMigrationServices()
            .AddSingleton(s =>
            {
                foreach (var (k, v) in Configuration)
                {
                    services.AddMigration(k, v);
                }
                return Configuration;
            });
            
        }

        public MigrationManagerModule(IMigrationManagerModuleConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            Console.WriteLine("Migration manager running");
            throw new NotImplementedException();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            Console.WriteLine("Migration manager stopping");
            throw new NotImplementedException();
        }
    }
}
