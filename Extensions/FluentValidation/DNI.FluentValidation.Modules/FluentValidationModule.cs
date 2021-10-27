using DNI.FluentValidation.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.FluentValidation.Modules
{
    public class FluentValidationModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            var options = moduleConfiguration.GetOptions<IFluentValidationModuleOptions>(ModuleDescriptor);
            serviceCollection.AddValidatorsFromAssemblies(options.Assemblies);
        }
    }
}
