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

        public static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMigrationServices();
                
            foreach(var (k,v) in Configuration)
            {
                services.AddMigration(k, v);
            }
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
