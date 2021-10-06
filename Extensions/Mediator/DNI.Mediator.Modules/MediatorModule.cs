using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Mediator.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace DNI.Mediator.Modules
{
    public class MediatorModule : ModuleBase
    {
        [Resolve] public static IMediatorModuleOptions Options { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(Options.ToArray());
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
