using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleRunner : IModule
    {
        void AddServiceConfiguration(Action<IServiceCollection, IModuleConfiguration> configureAction);
    }
}
