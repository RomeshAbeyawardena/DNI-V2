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
using System.Reflection;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;

namespace DNI.Mediator.Modules
{
    public class MediatorModule : ModuleBase
    {
        [Resolve] private static IDictionary<Assembly, IAssemblyOptions> AssemblyOptions { get; set; }
        [Resolve] private static IMediatorModuleOptions Options { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            var assemblies = Options.UseModuleAssemblies
                ? AssemblyOptions.Select(k => k.Key).ToArray()
                : Options.ToArray();

            services.AddMediatR(assemblies);
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
