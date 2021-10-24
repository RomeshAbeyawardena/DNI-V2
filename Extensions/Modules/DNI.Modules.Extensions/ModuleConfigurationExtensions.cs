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
        public static T GetOptions<TModule, T>(this IModuleConfiguration moduleConfiguration)
        {
            var opts = moduleConfiguration.Options
                .FirstOrDefault(a => a.Key.Type == typeof(TModule)).Value;

            return (T)opts;
        }

        public static T GetOptions<T>(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor)
        {
            if (moduleConfiguration.Options.TryGetValue(moduleDescriptor, out var options))
            {
                return (T)options;
            }

            return default;
        }

        public static IEnumerable<Assembly> GetModuleAssemblies(this IModuleConfiguration moduleConfiguration)
        {
            var assemblies = new List<Assembly>();

            foreach (var moduleType in moduleConfiguration.ModuleDescriptors)
            {
                var requiresDependenciesAttribute = moduleType.Type.GetCustomAttribute<RequiresDependenciesAttribute>();

                if (requiresDependenciesAttribute != null)
                {
                    assemblies.AddRange(requiresDependenciesAttribute.RequiredTypes.Select(a => a.Assembly));
                }
            }

            assemblies.AddRange(moduleConfiguration.ModuleDescriptors.Select(a => a.Type.Assembly).Distinct());

            return assemblies.Distinct();
        }

        public static void ConfigureOptions<T>(this IModuleConfiguration moduleConfiguration, IModuleDescriptor moduleDescriptor, T options)
        {
            var optionType = typeof(T);
            if (moduleConfiguration.Options.ContainsKey(moduleDescriptor))
            {
                moduleConfiguration.Options[moduleDescriptor] = options;
            }

            moduleConfiguration.Options.Add(moduleDescriptor, options);
        }

        public static IModuleRunner ConfigureRunner(this IModuleConfiguration moduleConfiguration, IServiceProvider serviceProvider, IServiceCollection services)
        {
            return new DefaultModuleRunner(services, serviceProvider, moduleConfiguration);
        }
    }
}
