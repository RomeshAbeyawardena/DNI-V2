using DNI.Extensions;
using DNI.Shared;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using DNI.Shared.Enumerations;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core
{
    /// <inheritdoc cref="IAppModule"/>
    public class DefaultAppModulesLoader : IAppModulesLoader
    {
        private readonly ILogger logger;
        private readonly ISerializerFactory serializerFactory;
        private readonly IFileProvider fileProvider;
        private readonly IServiceProvider serviceProvider;
        private readonly CancellationTokenSource cancellationTokenSource;

        public IEnumerable<IAppModule> Modules { get; private set; }

        public DefaultAppModulesLoader(
            ILogger logger,
            ISerializerFactory serializer,
            IFileProvider fileProvider,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serializerFactory = serializer;
            this.fileProvider = fileProvider;
            this.serviceProvider = serviceProvider;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public void Load(string moduleJsonPath, params string[] assemblyPaths)
        {
            Load(fileProvider
                .GetFile(moduleJsonPath)
                .ReadAllLines(), new DefaultAppModuleLoaderOptions { ModuleHintPaths = assemblyPaths });
        }

        public void Load(string json, IAppModulesLoaderOptions options)
        {
            var moduleConfig = serializerFactory
                .Deserialize<AppModuleConfig>(SerializerType.Json, json);

            LoadModules(moduleConfig, options);
        }

        private IEnumerable<Task> LoadModules(AppModuleConfig config, IAppModulesLoaderOptions options)
        {
            var files = options.ModuleHintPaths.SelectMany(hintPath => fileProvider.GetFiles(hintPath, "*.dll"));
            var moduleTaskList = new List<Task>();
            var assemblies = files.ForEach(file => Assembly.LoadFrom(file.FullPath));

            foreach (var module in config.Modules)
            {
                try
                {
                    var moduleType = Type.GetType(module.FullyQualifiedType);

                    var parameters = moduleType.GetConstructors(BindingFlags.Public)
                        .FirstOrDefault(a => a.GetCustomAttribute<DefaultConstructorAttribute>() != null)
                        .GetParameters()
                        .Select(a => serviceProvider.GetService(a.ParameterType));

                    var appModule = Activator.CreateInstance(moduleType, parameters) as IAppModule;

                    moduleTaskList.Add(appModule
                        .RunAsync(cancellationTokenSource.Token));
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Unable to load module {0}", module.FullyQualifiedType);
                    
                    if (!options.ContinueOnError)
                    {
                        throw;
                    }
                }
            }
            return moduleTaskList;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool gc)
        {
            if (gc)
            {
                cancellationTokenSource.Dispose();
            }
        }
    }
}
