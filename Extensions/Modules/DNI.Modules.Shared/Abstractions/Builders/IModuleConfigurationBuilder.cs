using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Abstractions.Builders
{
    public interface IModuleConfigurationBuilder
    {
        IModuleConfigurationBuilder AddModule(Type moduleType, Action<IModuleConfiguration> configure = null);
        IModuleConfiguration Build(IServiceProvider serviceProvider);
    }
}
