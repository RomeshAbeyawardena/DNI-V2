using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Core.Defaults
{
    public static class ValidationFailure
    {
        public static IValidationFailure Create(object model, Exception exception, string propertyName)
        {
            return new DefaultValidationFailure(model, exception, propertyName);
        }

        public static IValidationFailure Create(object model, Exception exception, PropertyInfo property = null)
        {
            return new DefaultValidationFailure(model, exception, property);
        }
    }

    internal class DefaultValidationFailure : IValidationFailure
    {
        public DefaultValidationFailure(object model, Exception exception, string propertyName)
            : this(model, exception)
        {
            if (!string.IsNullOrWhiteSpace(propertyName))
            {
                Property = model.GetType().GetProperty(propertyName);
            }
        }

       public DefaultValidationFailure(object model, Exception exception, PropertyInfo property = null)
       {
            Model = model;
            Exception = exception;
            Property = property;
        }

        public object Model { get; }

        public PropertyInfo Property { get; }

        public Exception Exception { get; }

        public object GetFailedValue()
        {
            return Property?.GetValue(Model);
        }
    }
}
