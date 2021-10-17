using DNI.Extensions;
using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Base
{
    public abstract class ModuleBase : SubscriptionManagerBase<IModuleStatus>, IModule
    {
        private readonly ISubject<IModuleStatus> moduleStatusSubject;

        public IObservable<IModuleStatus> Status => moduleStatusSubject;

        public Type ModuleType => GetType();

        public IEnumerable<Type> ModuleParameters => ModuleType.GetConstructorParameterTypes();
        
        public Guid UniqueId { get; set; }

        public IEnumerable<IDisposable> Disposables { get; set; }

        public virtual void OnDispose(bool disposing)
        {

        }

        public virtual void ConfigureBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {

        }

        public abstract void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration);
        public abstract Task OnStart(CancellationToken cancellationToken);
        public abstract Task OnStop(CancellationToken cancellationToken);

        public ModuleBase()
            : this(new Subject<IModuleStatus>())
        {

        }

        public ModuleBase(ISubject<IModuleStatus> moduleStatusSubject)
            : base(moduleStatusSubject)
        {
            this.moduleStatusSubject = new Subject<IModuleStatus>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            moduleStatusSubject.OnNext(new DefaultModuleStatus { IsRunning = true });
            return OnStart(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            moduleStatusSubject.OnNext(new DefaultModuleStatus { IsRunning = false });
            return OnStop(cancellationToken);
        }

        public override void Dispose(bool disposing)
        {
            Disposables.ForEach(d => d.Dispose());
            OnDispose(disposing);
        }
    }
}
