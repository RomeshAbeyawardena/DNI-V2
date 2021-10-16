using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleRunner : IModule
    {
        void AddServiceConfiguration(Action<IServiceCollection, IModuleConfiguration> configureAction);
    }
}
