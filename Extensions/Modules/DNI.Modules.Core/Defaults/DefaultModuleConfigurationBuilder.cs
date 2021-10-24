using DNI.Modules.Core.Defaults.Collections;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleConfigurationBuilder : IModuleConfigurationBuilder
    {
        private readonly IModuleConfiguration defaultModuleConfiguration;

        public List<IModuleDescriptor> moduleTypes;

        public DefaultModuleConfigurationBuilder(IModuleConfiguration moduleConfiguration)
        {
            moduleTypes = (moduleConfiguration.ModuleDescriptors != null && moduleConfiguration.ModuleDescriptors.Any())
                ? new(moduleConfiguration?.ModuleDescriptors)
                : new();

            defaultModuleConfiguration = moduleConfiguration;
        }

        public DefaultModuleConfigurationBuilder() : this(new DefaultModuleConfiguration())
        {

        }

        public IModuleConfigurationBuilder AddModule(IModuleDescriptor moduleType, Action<IModuleDescriptor, IModuleConfiguration> configure = null)
        {
            moduleTypes.Add(moduleType);
            configure?.Invoke(moduleType, defaultModuleConfiguration);
            return this;
        }

        public IModuleConfiguration Build(IServiceProvider serviceProvider)
        {
            defaultModuleConfiguration.ModuleDescriptors = new DefaultModuleDescriptorCollection(moduleTypes);
            defaultModuleConfiguration.ServiceProvider = serviceProvider;
            return defaultModuleConfiguration;
        }
    }
}
