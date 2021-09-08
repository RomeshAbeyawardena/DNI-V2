using DNI.Extensions;
using DNI.Shared;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using DNI.Shared.Enumerations;
using Microsoft.Extensions.DependencyInjection;
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
    public abstract class AppModuleLoaderBase : IAppModuleLoader
    {
        private readonly ILogger logger;
        private readonly ISerializerFactory serializerFactory;
        private readonly IFileProvider fileProvider;
        private IServiceProvider serviceProvider;
        private readonly IServiceCollection services;
        private readonly CancellationTokenSource cancellationTokenSource;

        public IEnumerable<IAppModule> Modules { get; private set; }
        public IEnumerable<Assembly> LoadedAssemblies { get; private set; }

        public AppModuleLoaderBase(
            ILogger logger,
            ISerializerFactory serializer,
            IFileProvider fileProvider)
        {
            this.logger = logger;
            this.serializerFactory = serializer;
            this.fileProvider = fileProvider;
            cancellationTokenSource = new CancellationTokenSource();
            this.services = new ServiceCollection();
        }

        public abstract void RegisterServices(IServiceCollection services);

        public void Load(string moduleJsonPath, params string[] assemblyPaths)
        {
            Load(fileProvider
                .GetFile(moduleJsonPath, System.IO.FileAccess.Read)
                .ReadAllLines(), new DefaultAppModuleLoaderOptions { ModuleHintPaths = assemblyPaths });
        }

        public void Load(string json, IAppModulesLoaderOptions options)
        {
            var moduleConfig = serializerFactory
                .Deserialize<AppModuleConfig>(SerializerType.Json, json);
            LoadedAssemblies = LoadAssemblies(options);

            RegisterServices(moduleConfig.Modules, out var moduleTypes);
            LoadModules(moduleTypes, options);
        }

        private IEnumerable<Assembly> LoadAssemblies(IAppModulesLoaderOptions options)
        {
            var files = options.ModuleHintPaths.SelectMany(hintPath => fileProvider.GetFiles(hintPath, "*.dll"));
            return files.ForEach(file => Assembly.LoadFrom(file.FullPath));

        }

        private void RegisterServices(IEnumerable<ModuleInfo> modules, out IEnumerable<Type> moduleTypes)
        {
            RegisterServices(services);
            var moduleTypeList = new List<Type>();
            moduleTypes = moduleTypeList;
            foreach (var module in modules)
            {
                var moduleType = Type.GetType(module.FullyQualifiedType);
                moduleTypeList.Add(moduleType);
                var registerServicesMethod = moduleType.GetMethod("RegisterServices", BindingFlags.Static);

                if(registerServicesMethod != null)
                {
                    registerServicesMethod.Invoke(null, new[] { services });
                }
            }

            serviceProvider = services.BuildServiceProvider();
        }

        private IEnumerable<Task> LoadModules(IEnumerable<Type> moduleTypes, IAppModulesLoaderOptions options)
        {
            var moduleTaskList = new List<Task>();
            foreach (var moduleType in moduleTypes)
            {
                try
                {
                    var parameters = moduleType.GetConstructors(BindingFlags.Public)
                        .FirstOrDefault(a => a.GetCustomAttribute<DefaultConstructorAttribute>() != null)
                        .GetParameters()
                        .Select(a => serviceProvider.GetService(a.ParameterType));

                    var appModule = Activator.CreateInstance(moduleType, parameters) as IAppModule;
                    
                }
                catch (Exception exception)
                {
                    logger.LogError(exception, "Unable to load module {0}", moduleType.FullName);
                    
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
            GC.SuppressFinalize(this);
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
