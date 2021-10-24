using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IValidationFailure
    {
        object Model { get; }
        PropertyInfo Property { get; }
        Exception Exception { get; }
        object GetFailedValue();
    }
}
