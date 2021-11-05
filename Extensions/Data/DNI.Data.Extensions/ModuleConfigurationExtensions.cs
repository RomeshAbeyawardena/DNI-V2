using DNI.Data.Core.Defaults;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Data.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureDbContextModuleOptions(
            this IModuleConfiguration moduleConfiguration,
            IModuleDescriptor moduleDescriptor,
            Action<IDbContextModuleOptionsBuilder> buildAction)
        {
            var defaultDbContextModuleOptionsBuilder = new DefaultDbContextModuleOptionsBuilder();
            buildAction(defaultDbContextModuleOptionsBuilder);

            var builtOptions = defaultDbContextModuleOptionsBuilder.Build();
            return moduleConfiguration.ConfigureOptions(moduleDescriptor, builtOptions);
        }

    }
}
