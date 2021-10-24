using DNI.Extensions;
using DNI.Modules.Core.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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

            foreach (var moduleDescriptor in moduleConfiguration.ModuleDescriptors.Where(a => a.Enabled))
            {
                if (configuredModules.Any(a => a.ModuleDescriptor == moduleDescriptor))
                {
                    continue;
                }

                logger.LogInformation("Configuring module {0} ({1})...", moduleDescriptor.Type, moduleDescriptor.Id);

                var module = serviceProvider.Activate<IModule>(moduleDescriptor.Type, out var disposables);
                module.ModuleDescriptor = moduleDescriptor;
                module.ConfigureModuleBuilder(services, moduleConfigurationBuilder);

                if (moduleConfiguration.ApplyConfiguration(moduleConfigurationBuilder, out newCount))
                {
                    hasChanged = true;
                }

                logger.LogInformation("{0} ({1}) assigned Id: {2}", moduleDescriptor.Type, moduleDescriptor.Id, module.UniqueId);

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
