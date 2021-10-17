using System;

namespace DNI.Modules.Shared.Abstractions.Builders
{
    public interface IModuleConfigurationBuilder
    {
        IModuleConfigurationBuilder AddModule(Type moduleType, Action<IModuleConfiguration> configure = null);
        IModuleConfiguration Build(IServiceProvider serviceProvider);
    }
}
