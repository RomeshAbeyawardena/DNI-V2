using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Abstractions.Builders;
using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class Startup : DisposableStartupBase
    {
        private readonly IMigrationConfigurator migrationConfiguratorOptionsBuilder;

        public Startup(IMigrationConfigurator migrationConfiguratorOptionsBuilder)
        {
            this.migrationConfiguratorOptionsBuilder = migrationConfiguratorOptionsBuilder;
        }

        public override void Dispose(bool disposing)
        {
            Console.WriteLine("Dispose called");
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync called");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StopAsync called");
            return Task.CompletedTask;
        }
    }
}
