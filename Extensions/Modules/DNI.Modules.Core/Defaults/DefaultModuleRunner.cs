using DNI.Core;
using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Modules.Shared.Extensions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    [RegisterService(ServiceLifetime.Transient)]
    public class DefaultModuleRunner : ModuleBase, IModuleRunner
    {
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly IServiceCollection services;
        private readonly IModuleOptions moduleOptions;
        private IEnumerable<IModule> modules;
        private IServiceProvider builtModuleServices;
        private readonly Dictionary<Type, IModule> modulesCache;
        private readonly List<IDisposable> subscribers;

        private IServiceProvider BuiltModuleServices
        {
            get
            {
                if (builtModuleServices == null)
                {
                    builtModuleServices = services.BuildServiceProvider();
                }

                return builtModuleServices;
            }
        }

        private static IEnumerable<Type> GetModuleTypes(IEnumerable<Assembly> assemblies)
        {
            return assemblies.SelectMany(a => a.GetTypes().Where(type => type.GetInterfaces().Any(interfaceType => interfaceType == typeof(IModule))));
        }

        private IModule Activate(Type type)
        {
            if (modulesCache.TryGetValue(type, out var module))
            {
                return module;
            }

            var defaultConstructor = type.GetConstructors().FirstOrDefault(a => a.IsPublic && a.IsConstructor);

            if (defaultConstructor == null)
            {
                module = Activator.CreateInstance(type) as IModule;
            }
            else
            {
                var parameters = defaultConstructor.GetParameters()
                    .Select(a => BuiltModuleServices.GetRequiredService(a.ParameterType))
                    .ToArray();

                module = Activator.CreateInstance(type, parameters) as IModule;
                
                module.AddParameters(parameters);
            }
            subscribers.Add(module.ResultState.Subscribe(OnNext, OnCompleted));
            subscribers.Add(module.State.Subscribe(moduleState));
            module.AddParameters(module.ResolveDependencies(BuiltModuleServices));

            return module;
        }

        private void OnCompleted(Exception obj)
        {
            throw obj;
        }

        private void OnNext(IModuleResult obj)
        {
            if (obj.IsException && obj.Haltable)
            {
                cancellationTokenSource.Cancel();
            }

            SetResult(obj);
        }

        private void RegisterServices(Type type)
        {
            //logger.LogInformation("Configuring services for {0}", type);

            AddParameters(type.ResolveStaticDependencies(BuiltModuleServices));
            var configureServicesMethod = type.GetMethod("ConfigureServices", BindingFlags.Public | BindingFlags.Static);

            var runTimeBindingAttribute = configureServicesMethod.GetCustomAttribute<RuntimeBindingAttribute>();

            if (runTimeBindingAttribute != null && !runTimeBindingAttribute.InvokeAtRunTime)
            {
                return;
            }

            configureServicesMethod?.Invoke(null, new[] { services });
        }

        public DefaultModuleRunner(IServiceCollection services, IModuleOptions moduleOptions)
        {
            cancellationTokenSource = new CancellationTokenSource();
            this.services = services;
            modulesCache = new Dictionary<Type, IModule>();
            subscribers = new List<IDisposable>();
            this.moduleOptions = moduleOptions;
            //this.logger = serviceProvider.GetService<ILogger<DefaultModuleRunner>>();
        }

        public void Configure(Action<IServiceCollection> configureServices)
        {
            configureServices(services);
        }

        public override async Task OnRun(CancellationToken cancellationToken)
        {
            services
                .AddSingleton(s => BuiltModuleServices)
                .AddSingleton(services);

            var injectableModules = GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.Injectable && a.Discoverable));

            var startupModules = GetModuleTypes(moduleOptions.ModuleAssembliesOptions.GetAssemblies(a => a.OnStartup && a.Discoverable));

            injectableModules.AppendAll(startupModules).ForEach(RegisterServices);

            var moduleDependencies = startupModules.SelectMany(a => a.GetCustomAttribute<RequiresDependenciesAttribute>()?.RequiredTypes ?? Array.Empty<Type>());

            var serviceModules = startupModules.Append(typeof(This))
                .AppendAll(moduleDependencies)
                .ToArray();

            services
                .Scan(s => s.FromAssembliesOf(serviceModules)
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Singleton))
                .AsImplementedInterfaces()
                .WithSingletonLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Scoped))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(a => a.WithAttribute<RegisterServiceAttribute>(rsa => rsa.ServiceLifetime == ServiceLifetime.Transient))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services
            .AddEncryptionServices()
            .AddSingleton(injectableModules.Select(Activate));
                ///.OutputServices();

            modules = startupModules.Select(Activate);
            await Task.WhenAll(modules.ForEach(m => m.Run(cancellationToken)));

        }

        public override async Task OnStop(CancellationToken cancellationToken)
        {
            await Task.WhenAll(modules.Select(a => a.Stop(cancellationToken)));
        }

        public override void Dispose(bool dispose)
        {
            if (dispose)
            {
                modules.ForEach(m => m.Dispose());
                subscribers.ForEach(m => m.Dispose());
            }

            base.Dispose(dispose);
        }

        public void Merge(IServiceCollection services)
        {
            foreach (var service in services)
            {
                if (!this.services.Contains(service))
                {
                    this.services.Add(service);
                }
            }
        }

    }
}
