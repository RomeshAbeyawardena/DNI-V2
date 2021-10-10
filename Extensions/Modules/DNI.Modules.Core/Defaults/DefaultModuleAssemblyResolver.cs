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
            if (!File.Exists(resolverOptions.JsonFileName))
                throw new FileNotFoundException("", resolverOptions.JsonFileName);

            var json = File.ReadAllText(resolverOptions.JsonFileName);

            resolverConfigurationOptions = JsonSerializer
                .Deserialize<DefaultModuleAssemblyResolverConfigurationOptions>(json);
        }

        public Assembly ResolveAssembly(string name)
        {
            var ass = typeof(DefaultModuleAssemblyResolver).Assembly.FullName;
            if(resolverConfigurationOptions.Modules.TryGetValue(name, out var assemblyOptions))
            {
                var path = Path.Combine(Environment.CurrentDirectory, assemblyOptions.FileName);
                if (File.Exists(path))
                {
                    return Assembly.LoadFrom(path);
                }
                
                return Assembly.Load(assemblyOptions.Id);
            }
            return null;
        }
    }
}
