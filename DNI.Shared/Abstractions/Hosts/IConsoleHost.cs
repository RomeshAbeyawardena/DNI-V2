using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Hosts
{
    public interface IConsoleHost : IDisposableStartup
    {
        IConsoleHost ConfigureServices<TStartup>(
            Action<IServiceCollection> configureServices)
            where TStartup : IStartup;
        IConsoleHost ConfigureServices(
            Type startupType,
            Action<IServiceCollection> configureServices);
    }
}
