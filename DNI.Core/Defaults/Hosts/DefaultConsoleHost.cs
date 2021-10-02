using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Hosts;
using DNI.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Core.Defaults.Hosts
{
    public static class ConsoleHost
    {
        public static IConsoleHost Build(Action<IConsoleHost> buildAction = default)
        {
            return DefaultConsoleHost.Build(buildAction);
        }
    }

    public class DefaultConsoleHost : DisposableBase, IConsoleHost
    {
        internal static IConsoleHost Build(Action<IConsoleHost> buildAction)
        {
            var consoleHost = new DefaultConsoleHost();
            buildAction?.Invoke(consoleHost);
            return consoleHost;
        }

        private bool hasStopBeenCalled = false;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly IServiceCollection services;
        private IServiceProvider ServiceProvider => services.BuildServiceProvider();
        private Type StartupType { get; set; }
        private IDisposable disposableService;

        private object InvokeServiceMethod(Type type, string methodName, BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public, params object[] parameters)
        {
            var startMethod = type.GetMethod(methodName, bindingFlags);
            var startupService = ServiceProvider.GetRequiredService(type);

            if(startupService is IDisposable disposable)
            {
                disposableService = disposable;
            }

            return startMethod?.Invoke(startupService, parameters);
        }

        private CancellationToken UseCancellationTokenSourceIfNull(CancellationToken cancellationToken)
        {
            if (cancellationToken == default)
            {
                return cancellationTokenSource.Token;
            }

            else return cancellationToken;
        }

        internal DefaultConsoleHost()
        {
            services = new ServiceCollection();
            cancellationTokenSource = new CancellationTokenSource();
        }

        public IConsoleHost ConfigureServices<TStartup>(
            Action<IServiceCollection> configureServices)
            where TStartup : IStartup
        {
            return ConfigureServices(typeof(TStartup), configureServices);
        }

        public IConsoleHost ConfigureServices(
            Type startupType,
            Action<IServiceCollection> configureServices)
        {
            this.StartupType = startupType;
            services.AddSingleton(startupType);
            configureServices(services);
            ConfigureServices(services => { });
            return this;
        }

        public override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!hasStopBeenCalled)
                {
                    Stop();
                }
                disposableService?.Dispose();
            }
        }

        public ValueTask DisposeAsync()
        {
            StopAsync(UseCancellationTokenSourceIfNull(default));
            hasStopBeenCalled = true;
            Dispose(true);
            return ValueTask.CompletedTask;
        }

        public void Start()
        {
            InvokeServiceMethod(StartupType, "Start");
        }

        public void Stop()
        {
            InvokeServiceMethod(StartupType, "Stop");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            return (Task)InvokeServiceMethod(StartupType, "StartAsync", parameters: UseCancellationTokenSourceIfNull(cancellationToken));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return (Task)InvokeServiceMethod(StartupType, "StopAsync", parameters: UseCancellationTokenSourceIfNull(cancellationToken));
        }

        public void ConfigureServices(Action<IServiceCollection> configureServices)
        {
            InvokeServiceMethod(StartupType, "ConfigureServices", BindingFlags.Public | BindingFlags.Static, services);
        }
    }
}
