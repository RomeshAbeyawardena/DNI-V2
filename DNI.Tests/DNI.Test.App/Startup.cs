﻿using DNI.MigrationManager.Modules;
using DNI.MigrationManager.Shared.Abstractions;
using DNI.MigrationManager.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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
        private readonly IModuleStartup moduleStartup;
        private readonly ILogger<Startup> logger;

        public Startup(IModuleStartup moduleStartup,
            ILogger<Startup> logger)
        {
            this.moduleStartup = moduleStartup;
            this.logger = logger;
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            
        }

        public override void Dispose(bool disposing)
        {
            logger.LogInformation("Dispose called");
            if (disposing)
            {
                moduleStartup.Dispose();
            }
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StartAsync called on {0}", Thread.CurrentThread.ManagedThreadId);
            return moduleStartup.Run(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("StopAsync called");
            return Task.CompletedTask;
            //return moduleStartup.Stop(cancellationToken);
        }
    }
}
