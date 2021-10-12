using DNI.Shared.Abstractions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class DisposableStartupBase : StartupBase, IDisposableStartup
    {
        public abstract void Dispose(bool disposing);

        public virtual ValueTask DisposeAsync(bool disposing)
        {
            Dispose();
            return ValueTask.CompletedTask;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public ValueTask DisposeAsync()
        {
            return DisposeAsync(true);
        }
    }
    public abstract class StartupBase : IStartup
    {
        public virtual void Start(params object[] args)
        {
            StartAsync(default)
                .Wait();
        }

        public abstract Task StartAsync(CancellationToken cancellationToken = default, params object[] args);

        public virtual void Stop()
        {
            StopAsync(default)
                .Wait();
        }

        public abstract Task StopAsync(CancellationToken cancellationToken = default);
    }
}
