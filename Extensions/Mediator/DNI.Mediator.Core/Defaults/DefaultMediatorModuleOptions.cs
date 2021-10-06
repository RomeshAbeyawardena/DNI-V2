using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Core.Defaults
{
    public class DefaultMediatorModuleOptions : CollectionBase<Assembly>, IMediatorModuleOptions
    {
        public DefaultMediatorModuleOptions(IEnumerable<Assembly> assemblies, bool useModuleAssemblies)
            : base(assemblies)
        {
            UseModuleAssemblies = useModuleAssemblies;
        }

        public bool UseModuleAssemblies { get; }
    }
}
