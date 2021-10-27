using DNI.FluentValidation.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.FluentValidation.Modules
{
    public class FluentValidationModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            var options = moduleConfiguration.GetOptions<IFluentValidationModuleOptions>(ModuleDescriptor);

            var assemblies = new List<Assembly>();

            if (options.UseModuleAssemblies)
            {
                assemblies.AddRange(moduleConfiguration.GetModuleAssemblies());
            }

            if (options.Assemblies != null && options.Assemblies.Any())
            {
                assemblies.AddRange(options.Assemblies);
            }

            serviceCollection.AddValidatorsFromAssemblies(assemblies);
        }
    }
}
