using DNI.Modules.Shared.Abstractions.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        IEnumerable<Assembly> GlobalAssemblies { get; set; }
        IEnumerable<ServiceDescriptor> ServiceDescriptors { get; set; }
        IServiceProvider ServiceProvider { get; set; }
        IDictionary<IModuleDescriptor, object> Options { get; }
        IModuleDescriptorCollection ModuleDescriptors { get; set; }
        ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider, IEnumerable<IModule> configuredModules, ILogger logger);
    }
}
