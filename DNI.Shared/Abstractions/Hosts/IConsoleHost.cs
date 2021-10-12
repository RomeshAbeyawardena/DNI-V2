using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Shared.Abstractions.Hosts
{
    /// <summary>
    /// Represents a console host
    /// </summary>
    public interface IConsoleHost : IDisposableStartup
    {
        /// <summary>
        /// Gets the list of <see cref="IServiceCollection"/>
        /// </summary>
        IServiceCollection Services { get; }

        /// <summary>
        /// Configures the <see cref="IServiceCollection"/> instance
        /// </summary>
        /// <typeparam name="TStartup">The startup class this instance of <see cref="IConsoleHost"/> will attempt to resolve</typeparam>
        /// <param name="configureServices">Configure services to be used by <see cref="IConsoleHost"/></param>
        /// <returns></returns>
        IConsoleHost ConfigureServices<TStartup>(
            Action<IServiceCollection> configureServices)
            where TStartup : IStartup;
        /// <summary>
        /// Configures the <see cref="IServiceCollection"/> instance
        /// </summary>
        /// <param name="startupType">The startup class this instance of <see cref="IConsoleHost"/> will attempt to resolve</param>
        /// <param name="configureServices">Configure services to be used by <see cref="IConsoleHost"/></param>
        /// <returns></returns>
        IConsoleHost ConfigureServices(
            Type startupType,
            Action<IServiceCollection> configureServices);

    }
}
