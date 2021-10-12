using DNI.Modules.Shared.Abstractions;
using System;
using System.Collections.Generic;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModulesConfiguration : IModulesConfiguration
    {
        public IEnumerable<DefaultModuleConfiguration> Modules { get; set; }
        public string IncludePath { get; set; }
        IEnumerable<IModuleConfiguration> IModulesConfiguration.Modules { get => Modules; set => throw new NotSupportedException(); }
    }
}
