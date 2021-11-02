using System.Collections.Generic;
using System.Reflection;

namespace DNI.FluentValidation.Shared.Abstractions
{
    public interface IFluentValidationModuleOptions
    {
        bool UseModuleAssemblies { get; }
        IEnumerable<Assembly> Assemblies { get; }
    }
}
