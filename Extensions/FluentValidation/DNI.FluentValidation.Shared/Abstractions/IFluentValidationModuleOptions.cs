using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.FluentValidation.Shared.Abstractions
{
    public interface IFluentValidationModuleOptions
    {
        bool UseModuleAssemblies { get; }
        IEnumerable<Assembly> Assemblies { get; }
    }
}
