using System.Collections.Generic;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModulesConfiguration : IIncludeConfiguration
    {
        IEnumerable<IModuleConfiguration> Modules { get; set; }
    }
}
