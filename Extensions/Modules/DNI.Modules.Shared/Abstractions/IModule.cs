using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModule : ISubscriptionManager<IModuleStatus>
    {
        IEnumerable<IDisposable> Disposables { get; set; }
        void SetUniqueId(Guid guid);
        Guid UniqueId { get; }
        Type ModuleType { get; }
        IEnumerable<Type> ModuleParameters { get; }
        IObservable<IModuleStatus> Status { get; }
        void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration);
        void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
