using DNI.Modules.Shared.Abstractions.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        IEnumerable<ServiceDescriptor> ServiceDescriptors { get; set; }
        IServiceProvider ServiceProvider { get; set; }
        IDictionary<IModuleDescriptor, object> Options { get; }
        IModuleDescriptorCollection ModuleDescriptors { get; set; }
        ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider, IEnumerable<IModule> configuredModules, ILogger logger);
    }
}
