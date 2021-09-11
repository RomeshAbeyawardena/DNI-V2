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
    public abstract class AppModuleLoaderBase<TAppModule> : IAppModuleLoader
        where TAppModule : class, IAppModuleLoader
    {
        private readonly ILogger logger;
        private readonly ISerializerFactory serializerFactory;
        private readonly IFileProvider fileProvider;
        private readonly IGlobalAppModuleCache<TAppModule> appModuleCache;
        private IServiceProvider serviceProvider;
        private readonly IServiceCollection services;
        private readonly CancellationTokenSource cancellationTokenSource;

        public IEnumerable<IAppModule> Modules { get; private set; }
        public IEnumerable<Assembly> LoadedAssemblies { get; private set; }

        public AppModuleLoaderBase(
            ILogger logger,
            ISerializerFactory serializer,
            IFileProvider fileProvider,
            IGlobalAppModuleCache<TAppModule> appModuleCache)
        {
            this.logger = logger;
            this.serializerFactory = serializer;
            this.fileProvider = fileProvider;
            this.appModuleCache = appModuleCache;
            cancellationTokenSource = new CancellationTokenSource();
            this.services = new ServiceCollection();
        }

        public abstract void RegisterServices(IGlobalAppModuleCache<TAppModule> appModuleCache, IServiceCollection services);

        public IEnumerable<IAppModule> Load(string moduleJsonPath, params string[] assemblyPaths)
        {
            return Load(fileProvider
                .GetFile(moduleJsonPath, System.IO.FileAccess.Read)
                .ReadAllLines(), new DefaultAppModuleLoaderOptions { ModuleHintPaths = assemblyPaths });
        }

        public IEnumerable<IAppModule> Load(string json, IAppModulesLoaderOptions options)
        {
            var moduleConfig = serializerFactory
                .Deserialize<AppModuleConfig>(SerializerType.Json, json);
            LoadedAssemblies = LoadAssemblies(options);

            RegisterServices(moduleConfig.Modules, out var moduleTypes);

            Modules = LoadModules(moduleTypes, options);
            return Modules;
        }

        private IEnumerable<Assembly> LoadAssemblies(IAppModulesLoaderOptions options)
        {
            var files = options.ModuleHintPaths.SelectMany(hintPath => fileProvider.GetFiles(hintPath, "*.dll"));
            return files.ForEach(file => Assembly.LoadFrom(file.FullPath));

        }

        private void InvokeRegisterServices(Type moduleType)
        {
            var registerServicesMethod = moduleType.GetMethod("RegisterServices", BindingFlags.Public | BindingFlags.Static);

            if (registerServicesMethod != null)
            {
                registerServicesMethod.Invoke(null, new object[] { appModuleCache, services });
            }
        }

        private void RegisterServices(IEnumerable<ModuleInfo> modules, out IEnumerable<Type> moduleTypes)
        {
            RegisterServices(appModuleCache, services);
            var moduleTypeList = new List<Type>();
            moduleTypes = moduleTypeList;

            foreach (var module in modules)
            {
                var moduleType = Type.GetType(module.FullyQualifiedType);
                moduleTypeList.Add(moduleType);
                InvokeRegisterServices(moduleType);
            }

            foreach (var appModule in appModuleCache)
            {
                InvokeRegisterServices(appModule);
            }


            serviceProvider = services.BuildServiceProvider();
        }

        private IEnumerable<IAppModule> LoadModules(IEnumerable<Type> moduleTypes, IAppModulesLoaderOptions options)
        {
            var moduleTaskList = new List<IAppModule>();
            foreach (var moduleType in moduleTypes)
            {
                try
                {
                    var ctor = moduleType.GetConstructors()
                        .FirstOrDefault();

                    var instanceParameters = new List<object>();

                    if (ctor != null)
                    {
                        var parameters = ctor.GetParameters();
                        instanceParameters.AddRange(parameters.Select(a => serviceProvider.GetService(a.ParameterType)));
                    }

                    var appModule = Activator.CreateInstance(moduleType, instanceParameters.ToArray()) as IAppModule;
                    moduleTaskList.Add(appModule);
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

        public IEnumerable<Task> RunAsync()
        {
            return Modules.ForEach(a => a.RunAsync(cancellationTokenSource.Token));
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
