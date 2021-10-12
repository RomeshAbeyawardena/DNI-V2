using System.Collections.Generic;
using System.Reflection;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IMediatorModuleOptions : IEnumerable<Assembly>
    {
        bool UseModuleAssemblies { get; }
    }
}
