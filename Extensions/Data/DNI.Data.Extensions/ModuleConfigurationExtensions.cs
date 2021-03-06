using DNI.Data.Core.Defaults;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Data.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureDbContextModule(
            this IModuleConfiguration moduleConfiguration,
            IModuleDescriptor moduleDescriptor,
            Action<IDbContextModuleOptionsBuilder> buildAction)
        {
            var defaultDbContextModuleOptionsBuilder = new DefaultDbContextModuleOptionsBuilder();
            buildAction(defaultDbContextModuleOptionsBuilder);

            var builtOptions = defaultDbContextModuleOptionsBuilder.Build();
            moduleConfiguration.ConfigureOptions(moduleDescriptor, builtOptions);
            return moduleConfiguration;
        }

    }
}
