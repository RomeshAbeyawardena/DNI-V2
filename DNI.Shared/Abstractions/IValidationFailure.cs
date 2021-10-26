using System;
using System.Reflection;

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
