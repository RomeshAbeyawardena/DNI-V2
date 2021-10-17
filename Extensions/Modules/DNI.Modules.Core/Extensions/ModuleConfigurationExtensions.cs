using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static bool ApplyConfiguration(this IModuleConfiguration moduleConfiguration, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            var ct = moduleConfiguration.ModuleTypes.Count();
            moduleConfigurationBuilder.Build(moduleConfiguration.ServiceProvider);
            var newct = moduleConfiguration.ModuleTypes.Count();

            return ct != newct;
        }
    }
}
