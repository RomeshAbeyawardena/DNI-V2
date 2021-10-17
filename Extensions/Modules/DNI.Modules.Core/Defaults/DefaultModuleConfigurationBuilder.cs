using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleConfigurationBuilder : IModuleConfigurationBuilder
    {
        private readonly IModuleConfiguration defaultModuleConfiguration;

        public List<Type> moduleTypes;

        public DefaultModuleConfigurationBuilder(IModuleConfiguration moduleConfiguration)
        {
            moduleTypes = (moduleConfiguration.ModuleTypes != null && moduleConfiguration.ModuleTypes.Any()) 
                ? new(moduleConfiguration?.ModuleTypes)
                : new();

            defaultModuleConfiguration = moduleConfiguration;
        }

        public DefaultModuleConfigurationBuilder() : this(new DefaultModuleConfiguration())
        {

        }

        public IModuleConfigurationBuilder AddModule(Type moduleType, Action<IModuleConfiguration> configure = null)
        {
            moduleTypes.Add(moduleType);
            configure?.Invoke(defaultModuleConfiguration);
            return this;
        }

        public IModuleConfiguration Build(IServiceProvider serviceProvider)
        {
            defaultModuleConfiguration.ModuleTypes = moduleTypes;
            defaultModuleConfiguration.ServiceProvider = serviceProvider;
            return defaultModuleConfiguration;
        }
    }
}
