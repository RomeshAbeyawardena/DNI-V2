using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModulesServiceCollection : IServiceCollection
    {
        event EventHandler ServiceAdded;
        event EventHandler ServiceBuilt;

        IServiceProvider BuildServiceProvider();
    }
}
