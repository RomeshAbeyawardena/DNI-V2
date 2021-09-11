using DNI.Extensions;
using DNI.ModuleLoader.Core.Base;
using DNI.Shared;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Factories;
using DNI.Shared.Attributes;
using DNI.Shared.Enumerations;
using DNI.Shared.Extensions;
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
        private IServiceProvider parentServiceProvider;
        private readonly IServiceCollection services;
        private readonly CancellationTokenSource cancellationTokenSource;

        private IEnumerable<Assembly> LoadAssemblies(IAppModulesLoaderOptions options)
        {
            var files = options.ModuleHintPaths.SelectMany(hintPath => fileProvider.GetFiles(hintPath, "*.dll"));
            return files.ForEach(file => Assembly.LoadFrom(file.FullPath));

        }

        
        private static bool InvokeUseGlobalAppModuleCache(Type moduleType)
        {
            var useGlobalAppModuleCacheMethod = moduleType.GetMethod("UseGlobalAppModuleCache", BindingFlags.Public | BindingFlags.Static);

            var useGlobalAppModuleCache = useGlobalAppModuleCacheMethod?.Invoke(null, null) as bool?;

            return useGlobalAppModuleCache.GetValueOrDefault(false);
        }

        private void InvokeRegisterServices(Type moduleType, bool isGlobal)
        {
            var appModuleCacheType = typeof(IAppModuleCache<>);

            var appModuleCacheInstance = !isGlobal
                ? parentServiceProvider.GetService(appModuleCacheType.MakeGenericType(moduleType))
                : appModuleCache;

            var registerServicesMethod = moduleType.GetMethod("RegisterServices", BindingFlags.Public | BindingFlags.Static);

            if (registerServicesMethod != null)
            {
                var appModuleConfigType = typeof(IAppModuleConfig<>);
                registerServicesMethod.Invoke(null, new object[] { appModuleCacheInstance, GetService(appModuleConfigType.MakeGenericType(moduleType)), services });
            }
        }

        private void ListModules(IEnumerable<Type> moduleTypes)
        {
            var stringBuilder = new StringBuilder();
            foreach(var moduleType in moduleTypes)
            {
                stringBuilder.AppendFormat("\r\n\t- {0}", moduleType);
            }

            logger.LogInformation(stringBuilder.ToString());
        }

        private IEnumerable<Type> RegisterAppModuleServices(IEnumerable<Type> moduleTypes)
        {
            var count = moduleTypes.Count();
            var moduleTypeList = new List<Type>();
            if (count > 0)
            {
                logger.LogInformation("Registering {0} modules...", count);
                ListModules(moduleTypes);
                foreach (var appModuleType in moduleTypes)
                {
                    var isGlobal = InvokeUseGlobalAppModuleCache(appModuleType);
                    InvokeRegisterServices(appModuleType, isGlobal);
                    moduleTypeList.Add(appModuleType);

                    var appModuleCacheType = typeof(IAppModuleCache<>);
                    var genericAppModuleCacheType = appModuleCacheType.MakeGenericType(appModuleType);
                    var appModuleCache = GetService(genericAppModuleCacheType) as IAppModuleCache;
                    logger.LogInformation("Registering child modules for {0}...", appModuleType);
                    ListModules(appModuleCache);
                    moduleTypeList.AddRange(RegisterAppModuleServices(appModuleCache));
                }
            }

            return moduleTypeList;
        }

        private void RegisterServices(IEnumerable<ModuleInfo> modules, out IEnumerable<Type> moduleTypes)
        {
            logger.LogInformation("Registering {0} native services...", typeof(TAppModule));
            RegisterServices(appModuleCache, services);
            var moduleTypeList = new List<Type>();
            moduleTypes = moduleTypeList;
            logger.LogInformation("{0} global modules ready to configured", modules.Count());
            logger.LogInformation("Module cache contains {0} modules", appModuleCache.Count());
            
            moduleTypeList.AddRange(
                RegisterAppModuleServices(appModuleCache));
            
            moduleTypeList.AddRange(
                RegisterAppModuleServices(modules.Select(module => Type.GetType(module.FullyQualifiedType))));

            serviceProvider = services.BuildServiceProvider();
        }

        private IEnumerable<IAppModule> LoadModules(IEnumerable<Type> moduleTypes, IAppModulesLoaderOptions options)
        {
            var appModuleBaseType = typeof(AppModuleBase<>);
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
                        instanceParameters.AddRange(parameters.Select(a => GetService(a.ParameterType)));
                    }

                    var appModule = Activator.CreateInstance(moduleType, instanceParameters.ToArray()) as IAppModule;

                    var moduleServiceProviderField = moduleType.BaseType.GetField("moduleServiceProvider", BindingFlags.NonPublic | BindingFlags.Instance);

                    if (moduleServiceProviderField != null)
                    {
                        moduleServiceProviderField.SetValue(appModule, new ModuleServiceProvider(parentServiceProvider, serviceProvider));
                    }
                    else
                    {
                        logger.LogWarning("{0} does not inherit from {1}, reliable dependency resolution will not be guaranteed and require its own implementation.", 
                            moduleType, appModuleBaseType.MakeGenericType(moduleType));
                    }

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

        private TService GetService<TService>()
            where TService : class
        {
            return GetService(typeof(TService)) as TService;
        }

        private object GetService(Type type)
        {
            return parentServiceProvider.GetService(type)
                ?? (serviceProvider != null
                    ? serviceProvider.GetService(type)
                    : default);
        }

        public IEnumerable<IAppModule> Modules { get; private set; }
        public IEnumerable<Assembly> LoadedAssemblies { get; private set; }

        public AppModuleLoaderBase(
            ILogger logger,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.parentServiceProvider = serviceProvider;
            this.serializerFactory = GetService<ISerializerFactory>();
            this.fileProvider = GetService<IFileProvider>();
            this.appModuleCache = GetService<IGlobalAppModuleCache<TAppModule>>();
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

            logger.LogInformation("Loading modules fron json, detected {0} modules", moduleConfig.Modules.Count());
            LoadedAssemblies = LoadAssemblies(options);

            logger.LogInformation("Registering services...");
            RegisterServices(moduleConfig.Modules, out var moduleTypes);

            Modules = LoadModules(moduleTypes, options);
            return Modules;
        }

        public IEnumerable<Task> RunAsync()
        {
            if (Modules.All(a => a.ValidateServices(serviceProvider)))
            {
                return Modules.ForEach(a => a.RunAsync(cancellationTokenSource.Token));
            }

            throw new InvalidOperationException("Service validation failed");
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
