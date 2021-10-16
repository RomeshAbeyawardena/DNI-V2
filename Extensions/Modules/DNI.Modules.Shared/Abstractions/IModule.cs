using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModule : ISubscriptionManager<IModuleStatus>
    {
        IEnumerable<IDisposable> Disposables { get; set; }
        
        Guid UniqueId { get; set; }
        Type ModuleType { get; }
        IEnumerable<Type> ModuleParameters { get; }
        IObservable<IModuleStatus> Status { get; }
        void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration);
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
