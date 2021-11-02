using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public abstract class ModuleBase<TOptions>
        : ModuleBase
    {
        protected readonly IModuleConfiguration moduleConfiguration;

        protected TOptions Options => moduleConfiguration.GetOptions<TOptions>(ModuleDescriptor);

        public ModuleBase(IModuleConfiguration moduleConfiguration)
        {
            this.moduleConfiguration = moduleConfiguration;
        }
    }
}
