using System;

namespace DNI.Shared.Base
{
    public abstract class DisposableBase : IDisposable
    {
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public abstract void Dispose(bool disposing);
    }
}
