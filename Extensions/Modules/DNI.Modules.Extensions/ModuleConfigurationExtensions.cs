using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static T GetOptions<T>(this IModuleConfiguration moduleConfiguration)
        {
            if(moduleConfiguration.Options.TryGetValue(typeof(T), out var options))
            {
                return (T)options;
            }

            return default;
        }

        public static IEnumerable<Assembly> GetModuleAssemblies(this IModuleConfiguration moduleConfiguration)
        {
            return moduleConfiguration.ModuleTypes.Select(a => a.Assembly).Distinct();
        }

        public static void ConfigureOptions<T>(this IModuleConfiguration moduleConfiguration, T options)
        {
            var optionType = typeof(T);
            if (moduleConfiguration.Options.ContainsKey(optionType))
            {
                moduleConfiguration.Options[optionType] = options;
            }

            moduleConfiguration.Options.Add(optionType, options);
        }

        public static IModuleRunner ConfigureRunner(this IModuleConfiguration moduleConfiguration, IServiceProvider services)
        {
            return new DefaultModuleRunner(services, moduleConfiguration);
        }
    }
}
