using DNI.Extensions;
using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
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

        /// <summary>
        /// Gets a list of assemblies for each module and any global assemblies configured by the instance of <see cref="Shared.Abstractions.Builders.IModuleConfigurationBuilder"/>
        /// </summary>
        /// <param name="moduleConfiguration"></param>
        /// <returns></returns>
        public static IEnumerable<Assembly> GetModuleAssemblies(this IModuleConfiguration moduleConfiguration)
        {
            var assemblies = new List<Assembly>();

            if(moduleConfiguration.GlobalAssemblies != null && moduleConfiguration.GlobalAssemblies.Any())
            {
                assemblies.AddRange(moduleConfiguration.GlobalAssemblies.Distinct());
            }

            foreach (var moduleType in moduleConfiguration.ModuleDescriptors)
            {
                assemblies.AddRange(moduleType.Type.GetRequiredDependencyAssemblies());
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
