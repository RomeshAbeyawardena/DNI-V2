using System;

namespace DNI.Modules.Shared.Abstractions.Builders
{
    public interface IModuleConfigurationBuilder
    {
        IModuleConfigurationBuilder AddModule(IModuleDescriptor moduleType, Action<IModuleDescriptor, IModuleConfiguration> configure = null);
        IModuleConfiguration Build(IServiceProvider serviceProvider);
    }
}
