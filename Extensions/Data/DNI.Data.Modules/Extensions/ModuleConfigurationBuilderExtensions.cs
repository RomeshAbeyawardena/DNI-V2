using DNI.Data.Extensions;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;

namespace DNI.Data.Modules
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureDbContextModule(this IModuleConfigurationBuilder builder, Action<IDbContextModuleOptionsBuilder> configure)
        {
            return builder.AddModule<DbContextModule>((moduleDescriptor, configuration) => configuration.ConfigureDbContextModuleOptions(moduleDescriptor, configure));
        }
    }
}
