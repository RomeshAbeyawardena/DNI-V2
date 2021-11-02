using DNI.Extensions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Mediator.Modules
{
    [RequiresDependencies(typeof(MediatorModule))]
    public class MediatorModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services,
            IModuleConfiguration moduleConfiguration)
        {
            var options = moduleConfiguration.GetOptions<IMediatorModuleOptions>(ModuleDescriptor);

            List<Assembly> assemblyList = new(typeof(MediatorModule).GetRequiredDependencyAssemblies());
            assemblyList.AddRange(options);

            if (options.UseModuleAssemblies)
            {
                assemblyList.AddRange(moduleConfiguration.GetModuleAssemblies());
            }

            services
                .AddMediatR(assemblyList.Distinct().ToArray())
                .OutputServices();
        }
    }
}
