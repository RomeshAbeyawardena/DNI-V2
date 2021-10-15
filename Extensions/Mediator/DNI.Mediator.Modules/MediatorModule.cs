using DNI.Modules.Shared.Base;
using DNI.Mediator.Shared.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Extensions;

namespace DNI.Mediator.Modules
{
    public class MediatorModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var options = moduleConfiguration.GetOptions<IMediatorModuleOptions>();
            var assemblies = options.UseModuleAssemblies
                ? moduleConfiguration.GetModuleAssemblies()
                : options;

            services.AddMediatR(assemblies.ToArray());
        }

        public override void OnDispose(bool disposing)
        {
            
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
