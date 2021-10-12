﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    /// <summary>
    /// Represents a module to run a unit of work
    /// </summary>
    public interface IModule : IDisposable
    {
        bool IsStopped { get; }
        IObservable<ModuleEventArgs> State { get; }
        /// <summary>
        /// Adds constructor parameters
        /// </summary>
        /// <param name="parameters"></param>
        void AddParameters(IEnumerable<object> parameters);
        /// <summary>
        /// Runs a unit of work within this <see cref="IModule"/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Run(CancellationToken cancellationToken);
        /// <summary>
        /// Stops the unit of work prior completion
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Stop(CancellationToken cancellationToken);

        IObservable<IModuleResult> ResultState { get; }
    }
}
