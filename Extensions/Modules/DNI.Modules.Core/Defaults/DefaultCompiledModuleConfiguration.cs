using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultCompiledModuleConfiguration : ICompiledModuleConfiguration
    {
        public DefaultCompiledModuleConfiguration(IDictionary<Type, object> options)
        {
            Options = options;
        }

        public IDictionary<Type, object> Options { get; }
        public IEnumerable<IModule> Modules { get; set; }
    }
}
