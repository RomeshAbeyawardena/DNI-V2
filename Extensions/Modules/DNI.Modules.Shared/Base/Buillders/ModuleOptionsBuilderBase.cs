using DNI.Modules.Shared.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Base.Buillders
{
    public abstract class ModuleOptionsBuilderBase<TOptions> : IModuleOptionsBuilder<TOptions>
    {
        public abstract TOptions Build();
    }
}
