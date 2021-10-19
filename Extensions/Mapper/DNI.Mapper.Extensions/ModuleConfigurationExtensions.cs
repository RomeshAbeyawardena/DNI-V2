using DNI.Mapper.Core.Defaults;
using DNI.Mapper.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Mapper.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureMapperModule(this IModuleConfiguration moduleConfiguration, Action<IMapperOptionsBuilder> configure)
        {
            var encryptionOptionsBuilder = new DefaultMapperOptionsBuilder();
            configure(encryptionOptionsBuilder);
            moduleConfiguration.ConfigureOptions(encryptionOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
