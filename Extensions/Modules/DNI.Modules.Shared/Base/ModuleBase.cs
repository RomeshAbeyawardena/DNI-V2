using DNI.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Defaults;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Base
{
    public abstract class ModuleBase : DisposableBase, IModule
    {
        private readonly object syncRoot;
        private readonly List<object> parameters;
        private readonly ISubject<IModuleResult> resultState;
        private bool isStopped;
        protected readonly ISubject<ModuleEventArgs> moduleState;

        public bool IsStopped {
            get { lock (syncRoot) return isStopped; }
            set
            {
                lock (syncRoot)
                {
                    isStopped = value;
                }
            }
        }

        public IObservable<ModuleEventArgs> State => moduleState;

        protected IModuleResult ReportError(Exception exception)
        {
            OnError(exception);
            return DefaultModuleResult.Failed(exception);
        }

        protected ModuleBase()
        {
            syncRoot = new object();
            resultState = new Subject<IModuleResult>();
            parameters = new List<object>();
            moduleState = new Subject<ModuleEventArgs>();
        }

        protected void SetResult(IModuleResult result)
        {
            resultState.OnNext(result);
        }

        public virtual void AddParameters(IEnumerable<object> parameters)
        {
            this.parameters.AddRange(parameters);
        }

        protected void OnStarted(ModuleEventArgs e, CancellationToken cancellationToken)
        {
            OnRun(cancellationToken);
            moduleState.OnNext(e);
        }

        protected void OnStopped(ModuleEventArgs e, CancellationToken cancellationToken)
        {
            if (!IsStopped)
            {
                OnStop(cancellationToken);
                moduleState.OnNext(e);
                moduleState.OnCompleted();
                IsStopped = true;
            }
        }

        public void OnError(Exception exception)
        {
            moduleState.OnError(exception);
        }

        /// <inheritdoc cref="IDisposable"/>
        public override void Dispose(bool dispose)
        {
            if (dispose)
            {
                Stop(CancellationToken.None).Wait();

                parameters.Where(a => a is IDisposable)
                    .Select(a => a as IDisposable)
                    .ForEach(a => a.Dispose());
            }
        }

        public Task Run(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            OnStarted(new ModuleEventArgs(this, true), cancellationToken);
            return Task.CompletedTask;
        }

        public Task Stop(CancellationToken cancellationToken)
        {
            OnStopped(new ModuleEventArgs(this, false), cancellationToken);
            return Task.CompletedTask;
        }

        public abstract Task OnRun(CancellationToken cancellationToken);
        public abstract Task OnStop(CancellationToken cancellationToken);

        public IObservable<IModuleResult> ResultState => resultState;
    }
}
