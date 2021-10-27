using DNI.FluentValidation.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.FluentValidation.Core.Defaults
{
    public class DefaultFluentValidationModuleOptions : IFluentValidationModuleOptions
    {
        public IEnumerable<Assembly> Assemblies { get; init; }
    }
}
