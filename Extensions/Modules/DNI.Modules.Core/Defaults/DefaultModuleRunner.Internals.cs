using DNI.Extensions;
using DNI.Modules.Core.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal partial class DefaultModuleRunner
    {
        private ICompiledModuleConfiguration ConfigureModuleConfiguration(IServiceProvider serviceProvider)
        {
            return moduleConfiguration.Compile(serviceProvider, configuredModules, logger);
        }

        private Task OnStartModule(IModule module, CancellationToken cancellationToken)
        {
            if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                module.Disposables = disposables;
            return module.StartAsync(cancellationToken);
        }

        private bool ConfigureModulesBuilder(IServiceCollection services, 
            IModuleConfigurationBuilder moduleConfigurationBuilder, out int newCount)
        {
            newCount = 0;
            var hasChanged = false;

            foreach (var moduleType in moduleConfiguration.ModuleDescriptors)
            {
                if (configuredModules.Any(a => a.ModuleDescriptor == moduleType))
                {
                    continue;
                }

                logger.LogInformation("Configuring module {0} ({1})...", moduleType.Type, moduleType.Id);

                var module = serviceProvider.Activate<IModule>(moduleType.Type, out var disposables);
                module.ModuleDescriptor = moduleType;
                module.ConfigureModuleBuilder(services, moduleConfigurationBuilder);

                if (moduleConfiguration.ApplyConfiguration(moduleConfigurationBuilder, out newCount))
                {
                    hasChanged = true;
                }

                logger.LogInformation("{0} ({1}) assigned Id: {2}", moduleType.Type, moduleType.Id, module.UniqueId);

                disposableTypesList.Add(module.UniqueId, disposables);
                configuredModules.Add(module);
            }

            return hasChanged;
        }

        private void ConfigureModules(IModuleConfiguration moduleConfiguration)
        {
            foreach (var module in configuredModules)
            {
                module.ConfigureServices(services, moduleConfiguration);
                if (disposableTypesList.TryGetValue(module.UniqueId, out var disposables))
                {
                    module.Disposables = disposables;
                }
            }
        }

    }
}
