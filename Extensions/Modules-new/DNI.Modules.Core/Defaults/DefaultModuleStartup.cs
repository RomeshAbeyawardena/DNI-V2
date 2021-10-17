﻿using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleStartup : ModuleBase, IModuleStartup
    {
        private readonly IModuleRunner moduleRunner;

        public DefaultModuleStartup(
            IModuleConfiguration moduleConfiguration,
            IModuleRunner moduleRunner)
        {
            Configuration = moduleConfiguration;
            this.moduleRunner = moduleRunner;
        }

        public IModuleConfiguration Configuration { get; }

        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            moduleRunner.ConfigureServices(serviceCollection, moduleConfiguration);
        }

        public override void OnDispose(bool disposing)
        {
            moduleRunner.Dispose();
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            return moduleRunner.StartAsync(cancellationToken);
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return moduleRunner.StopAsync(cancellationToken);
        }
    }
}