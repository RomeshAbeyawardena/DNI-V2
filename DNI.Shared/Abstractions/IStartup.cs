using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    /// <summary>
    /// Represents a startup that can run a sequence of related code
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Starts the current <see cref="IStartup"/> instance
        /// </summary>
        void Start(params object[] args);
        /// <summary>
        /// Stops the current <see cref="IStartup"/> instance
        /// </summary>
        void Stop();

        /// <summary>
        /// Starts the current <see cref="IStartup"/> instance
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> that relates to the current async instance</returns>
        Task StartAsync(CancellationToken cancellationToken = default, params object[] args);
        /// <summary>
        /// Stops the current <see cref="IStartup"/> instance
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> that relates to the current async instance</returns>
        Task StopAsync(CancellationToken cancellationToken = default);
    }
}
