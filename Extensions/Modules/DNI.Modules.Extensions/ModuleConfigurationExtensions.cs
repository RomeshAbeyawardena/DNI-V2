using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DNI.Modules.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static T GetOptions<T>(this IModuleConfiguration moduleConfiguration)
        {
            if (moduleConfiguration.Options.TryGetValue(typeof(T), out var options))
            {
                return (T)options;
            }

            return default;
        }

        public static IEnumerable<Assembly> GetModuleAssemblies(this IModuleConfiguration moduleConfiguration)
        {
            var assemblies = new List<Assembly>();

            foreach (var moduleType in moduleConfiguration.ModuleTypes)
            {
                var requiresDependenciesAttribute = moduleType.GetCustomAttribute<RequiresDependenciesAttribute>();

                if (requiresDependenciesAttribute != null)
                {
                    assemblies.AddRange(requiresDependenciesAttribute.RequiredTypes.Select(a => a.Assembly));
                }
            }

            assemblies.AddRange(moduleConfiguration.ModuleTypes.Select(a => a.Assembly).Distinct());

            return assemblies.Distinct();
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

        public static IModuleRunner ConfigureRunner(this IModuleConfiguration moduleConfiguration, IServiceProvider serviceProvider, IServiceCollection services)
        {
            return new DefaultModuleRunner(services, serviceProvider, moduleConfiguration);
        }
    }
}
