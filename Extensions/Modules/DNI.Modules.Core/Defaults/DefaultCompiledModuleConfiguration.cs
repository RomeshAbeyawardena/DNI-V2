using DNI.Modules.Shared.Abstractions;
using System.Collections.Generic;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultCompiledModuleConfiguration : ICompiledModuleConfiguration
    {
        public DefaultCompiledModuleConfiguration(IDictionary<IModuleDescriptor, object> options)
        {
            Options = options;
        }

        public IDictionary<IModuleDescriptor, object> Options { get; }
        public IEnumerable<IModule> Modules { get; set; }
    }
}
