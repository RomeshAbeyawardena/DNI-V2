using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleConfiguration : IModuleConfiguration
    {
        private readonly Dictionary<Type, object> options;
#pragma warning disable IDE0052 // Remove unread private members
        private IEnumerable<IDisposable> disposables;
#pragma warning restore IDE0052 // Remove unread private members

        public DefaultModuleConfiguration()
        {
            options = new Dictionary<Type, object>();
        }

        public IServiceProvider ServiceProvider { get; set; }

        public IEnumerable<Type> ModuleTypes { get; set; }

        public IDictionary<Type, object> Options => options;

        public ICompiledModuleConfiguration Compile(IServiceProvider serviceProvider, IEnumerable<IModule> configuredModules, ILogger logger)
        {
            
            logger.LogInformation("Compiling modules for startup...");
            StringBuilder stringBuilder = new("Modules: \tUnique Id\t\t\t\tName\r\n");
            var activatedModuleList = new List<IModule>();
            foreach (var module in configuredModules)
            {
                stringBuilder.AppendLine($"   [x]   \t{module.UniqueId}\t{module.ModuleType.Name}");
                var moduleType = module.GetType();

                if (ModuleTypes.Contains(moduleType))
                {
                    var activatedModule = serviceProvider.Activate<IModule>(module.GetType(), out disposables);
                    activatedModule.SetUniqueId(module.UniqueId);
                    activatedModuleList.Add(activatedModule);
                }
            }

            logger.LogInformation(stringBuilder.ToString());

            return new DefaultCompiledModuleConfiguration(options) { Modules = activatedModuleList };
        }
    }
}
