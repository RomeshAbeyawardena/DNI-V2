using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
