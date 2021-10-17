using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModulesServiceCollection : IServiceCollection
    {
        event EventHandler ServiceAdded;
        event EventHandler ServiceBuilt;

        IServiceProvider BuildServiceProvider();
    }
}
