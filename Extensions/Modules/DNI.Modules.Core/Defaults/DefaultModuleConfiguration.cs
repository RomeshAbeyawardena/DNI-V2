using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Collections;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleConfiguration : IModuleConfiguration
    {
        private readonly Dictionary<IModuleDescriptor, object> options;

        public DefaultModuleConfiguration()
        {
            options = new Dictionary<IModuleDescriptor, object>();
        }

        public IEnumerable<Assembly> GlobalAssemblies { get; set; }

        public IEnumerable<ServiceDescriptor> ServiceDescriptors { get; set; }

        public IServiceProvider ServiceProvider { get; set; }

        public IModuleDescriptorCollection ModuleDescriptors { get; set; }

        public IDictionary<IModuleDescriptor, object> Options => options;

        public ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider, IEnumerable<IModule> configuredModules, ILogger logger)
        {
            
            logger.LogInformation("Compiling modules for startup...");
            StringBuilder stringBuilder = new("Modules: \tUnique Id\t\t\t\tName\r\n");
            var activatedModuleList = new List<IModule>();
            foreach (var module in configuredModules)
            {
                stringBuilder.AppendLine($"   [x]   \t{module.UniqueId}\t{module.ModuleType.Name} ({module.ModuleDescriptor.Id})");
                
                if (ModuleDescriptors.Contains(module.ModuleDescriptor))
                {
                    var activatedModule = serviceProvider.Activate<IModule>(module.GetType(), out var disposables);
                    activatedModule.ModuleDescriptor = module.ModuleDescriptor;
                    activatedModule.SetUniqueId(module.UniqueId);
                    activatedModuleList.Add(activatedModule);
                }
            }

            logger.LogInformation(stringBuilder.ToString());

            return new DefaultCompiledModuleConfiguration(options) { Modules = activatedModuleList };
        }
    }
}
