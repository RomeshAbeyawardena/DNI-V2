using DNI.Extensions;
using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reactive.Subjects;
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

        public Guid UniqueId { get; private set; }

        public IEnumerable<IDisposable> Disposables { get; set; }

        public IModuleDescriptor ModuleDescriptor { get; set; }

        public virtual void OnDispose(bool disposing)
        {

        }

        public virtual void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {

        }

        public virtual void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {

        }

        public virtual Task OnStart(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public virtual Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public ModuleBase()
            : this(new Subject<IModuleStatus>())
        {
            UniqueId = Guid.NewGuid();
        }

        public ModuleBase(ISubject<IModuleStatus> moduleStatusSubject)
            : base(moduleStatusSubject)
        {
            this.moduleStatusSubject = new Subject<IModuleStatus>();
        }

        public void SetUniqueId(Guid uniqueId)
        {
            UniqueId = uniqueId;
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
