using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Shared.Abstractions;
using System;

namespace DNI.Modules.Extensions
{
    public static class MigrationConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddGlobalAssembly(this IModuleConfigurationBuilder moduleConfigurationBuilder, Action<IAssemblyOptionsBuilder> buildAssemblies)
        {
            var assemblyOptionsBuilder = new DefaultAssemblyOptionsBuilder();
            buildAssemblies?.Invoke(assemblyOptionsBuilder);
            moduleConfigurationBuilder.GlobalAssemblies = assemblyOptionsBuilder.BuildAssemblies();
            return moduleConfigurationBuilder;
        }
        public static IModuleConfigurationBuilder AddModule<T>(this IModuleConfigurationBuilder builder, Action<IModuleDescriptor, IModuleConfiguration> configure = null)
        {
            var moduleDescriptor = ModuleDescriptor.Create<T>(ModuleDescriptor.DefaultUsage);
            return builder.AddModule(moduleDescriptor, configure);
        }
    }
}
