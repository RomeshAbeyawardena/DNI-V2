using DNI.Mapper.Extensions;
using DNI.Mapper.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mapper.Modules.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureMapperModule(this IModuleConfigurationBuilder moduleConfigurationBuilder,
           Action<IMapperOptionsBuilder> configure)
        {
            return moduleConfigurationBuilder
                .AddModule<MapperModule>(configuration => configuration.ConfigureMapperModule(configure));
        }
    }
}
