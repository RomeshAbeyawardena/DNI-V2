using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleRunner ConfigureRunner(this IModuleConfiguration moduleConfiguration, IServiceProvider services)
        {
            return new DefaultModuleRunner(services, moduleConfiguration);
        }
    }
}
