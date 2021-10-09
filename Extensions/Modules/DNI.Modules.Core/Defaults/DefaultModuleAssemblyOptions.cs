using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Extensions;
using DNI.Shared.Base;
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
    public class DefaultModuleAssemblyOptions : DictionaryBase<Assembly, IAssemblyOptions>,
        IModuleAssemblyOptions,
        IModuleAssemblyLocatorOptions
    {
        private IModuleAssemblyResolver moduleAssemblyResolver = DefaultModuleAssemblyResolver.Default;

        public void ConfigureResolver(IModuleAssemblyResolverOptions moduleAssemblyResolverOptions)
        {
            moduleAssemblyResolver = new DefaultModuleAssemblyResolver(moduleAssemblyResolverOptions);
        }

        private void AddAssemblyByConfiguration(IModuleConfiguration moduleConfiguration)
        {
            if (moduleConfiguration.Enabled)
            {
                AddAssembly(moduleConfiguration.AssemblyName ?? moduleConfiguration.FileName, moduleConfiguration.Options);
            }
        }

        public IModuleAssemblyOptions AddAssembly(Assembly assembly, IAssemblyOptions assemblyOptions)
        {
            Add(assembly, assemblyOptions);
            return this;
        }

        public IModuleAssemblyOptions AddAssembly<T>(IAssemblyOptions assemblyOptions)
        {
            return AddAssembly(typeof(T).Assembly, assemblyOptions);
        }

        public IModuleAssemblyLocatorOptions AddAssembly(string assemblyNameorFilePath, IAssemblyOptions assemblyOptions)
        {
            var assembly = moduleAssemblyResolver.ResolveAssembly(assemblyNameorFilePath);

            if(assembly != null)
            {
                AddAssembly(assembly, assemblyOptions);
                return this;
            }

            AddAssembly(File.Exists(assemblyNameorFilePath)
                ? Assembly.LoadFrom(assemblyNameorFilePath)
                : Assembly.Load(assemblyNameorFilePath), assemblyOptions);
            return this;
        }

        public IModuleAssemblyLocatorOptions AddAssembly(string appSettingsSection, string fileName = null)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                fileName = "modules.json";
            }

            using (var streamReader = File.OpenText(fileName))
            {
                var json = streamReader.ReadToEnd();
                IModulesConfiguration configuration = JsonSerializer.Deserialize<DefaultModulesConfiguration>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    .Extend<DefaultModulesConfiguration>();

                var modules = configuration.Modules;

                modules.ForEach(AddAssemblyByConfiguration);
            }

            return this;
        }

        public IEnumerable<Assembly> GetAssemblies(Func<IAssemblyOptions, bool> filterOptions)
        {
            return dictionary
                .Where((k) => filterOptions(k.Value))
                .Select(a => a.Key);
        }
    }
}
