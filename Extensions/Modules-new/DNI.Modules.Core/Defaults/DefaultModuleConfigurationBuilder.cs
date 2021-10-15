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
        public List<Type> moduleTypes;

        public DefaultModuleConfigurationBuilder()
        {
            moduleTypes = new();
        }

        public IModuleConfigurationBuilder AddModule(Type moduleType)
        {
            moduleTypes.Add(moduleType);
            return this;
        }

        public IModuleConfiguration Build(IServiceProvider serviceProvider)
        {
            var defaultModuleConfiguration = new DefaultModuleConfiguration { ModuleTypes = moduleTypes };

            return defaultModuleConfiguration;
        }
    }
}
