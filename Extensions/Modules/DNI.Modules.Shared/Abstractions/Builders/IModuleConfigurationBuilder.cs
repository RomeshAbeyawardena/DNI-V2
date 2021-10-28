using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Modules.Shared.Abstractions.Builders
{
    public interface IModuleConfigurationBuilder
    {
        IEnumerable<Assembly> GlobalAssemblies { get; set; }

        IModuleConfigurationBuilder AddModule(IModuleDescriptor moduleType, Action<IModuleDescriptor, IModuleConfiguration> configure = null);
        IModuleConfiguration Build(IServiceProvider serviceProvider);
    }
}
