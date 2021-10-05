using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    /// <summary>
    /// Represents a <see cref="IStartup"/> that implements the <see cref="IDisposable"/> pattern
    /// </summary>
    public interface IDisposableStartup : IStartup, IDisposable, IAsyncDisposable
    {
        
    }
}
