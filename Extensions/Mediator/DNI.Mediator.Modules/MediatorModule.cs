using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace DNI.Mediator.Modules
{
    public class MediatorModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            var options = moduleConfiguration.GetOptions<IMediatorModuleOptions>(ModuleDescriptor);
            var assemblies = options.UseModuleAssemblies
                ? moduleConfiguration.GetModuleAssemblies()
                : options;

            services.AddMediatR(assemblies.ToArray());
        }
    }
}
