using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    /// <summary>
    /// Represents an app module that can be run in parallel with other modules
    /// </summary>
    public interface IAppModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        void RegisterServices(IServiceCollection services);
        bool ValidateServices(IServiceProvider serviceProvider);
        Task RunAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}
