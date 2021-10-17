using System;

namespace DNI.Shared.Abstractions
{
    /// <summary>
    /// Represents a <see cref="IStartup"/> that implements the <see cref="IDisposable"/> pattern
    /// </summary>
    public interface IDisposableStartup : IStartup, IDisposable, IAsyncDisposable
    {

    }
}
