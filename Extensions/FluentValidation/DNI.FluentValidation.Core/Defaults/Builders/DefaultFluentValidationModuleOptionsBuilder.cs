using DNI.Modules.Shared.Base.Buillders;
using DNI.FluentValidation.Shared.Abstractions;
using DNI.FluentValidation.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Modules.Shared.Base.Builders;
using System.Reflection;

namespace DNI.FluentValidation.Core.Defaults.Builders
{
    public class DefaultFluentValidationModuleOptionsBuilder : ModuleOptionsAssemblyBuilderBase<IFluentValidationModuleOptions>, IFluentValidationModuleOptionsBuilder
    {
        public override IFluentValidationModuleOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            throw new NotImplementedException();
        }
    }
}
