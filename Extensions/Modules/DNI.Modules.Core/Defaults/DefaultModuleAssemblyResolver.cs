using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleAssemblyResolver : IModuleAssemblyResolver
    {
        private readonly DefaultModuleAssemblyResolverConfigurationOptions resolverConfigurationOptions;

        public static IModuleAssemblyResolver Default => new DefaultModuleAssemblyResolver(new DefaultModuleAssemblyResolverOptions { JsonFileName = "default-modules.json" });

        public DefaultModuleAssemblyResolver(IModuleAssemblyResolverOptions resolverOptions)
        {
            resolverConfigurationOptions = JsonSerializer.Deserialize<DefaultModuleAssemblyResolverConfigurationOptions>(resolverOptions.JsonFileName);
        }

        public Assembly ResolveAssembly(string name)
        {
            if(resolverConfigurationOptions.Modules.TryGetValue(name, out var assemblyName))
            {
                if (File.Exists(assemblyName))
                {
                    return Assembly.LoadFrom(assemblyName);
                }
                
                return Assembly.Load(assemblyName);
            }

            return null;
        }
    }
}
