using DNI.FluentValidation.Shared.Abstractions;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.FluentValidation.Core.Defaults
{
    public class DefaultFluentValidationModuleOptions : IFluentValidationModuleOptions
    {
        public IEnumerable<Assembly> Assemblies { get; init; }
        public bool UseModuleAssemblies { get; internal set; }
    }
}
