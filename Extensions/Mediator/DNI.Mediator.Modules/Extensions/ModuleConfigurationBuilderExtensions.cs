using DNI.Mediator.Extensions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureMediatorModule(this IModuleConfigurationBuilder builder, Action<IMediatorModuleOptionsBuilder> configure)
        {
            return builder.AddModule<MediatorModule>(configuration => configuration
                .ConfigureMediatorModule(configure));
        }
    }
}
