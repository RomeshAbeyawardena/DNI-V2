using DNI.Core.Defaults;
using DNI.Shared.Abstractions.Collections;
using System;
using System.Reflection;

namespace DNI.Extensions
{
    public static class ValidationFailureCollectionExtensions
    {
        public static void Add(this IValidationFailureCollection validationFailureCollection, object model, Exception exception, string propertyName)
        {
            validationFailureCollection.Add(ValidationFailure.Create(model, exception, propertyName));
        }

        public static void Add(this IValidationFailureCollection validationFailureCollection, object model, Exception exception, PropertyInfo property)
        {
            validationFailureCollection.Add(ValidationFailure.Create(model, exception, property));
        }

        public static void Add(this IValidationFailureCollection validationFailureCollection, object model, string exception, string propertyName)
        {
            Add(validationFailureCollection, model, new Exception(exception), propertyName);
        }
    }
}
