using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Collections;
using DNI.Shared.Defaults.Collections;
using System;
using System.Collections.Generic;

namespace DNI.Shared.Exceptions
{
    public class ModelStateException : Exception
    {
        public ModelStateException(object model, IEnumerable<IValidationFailure> validationFailures)
        {
            Model = model;
            ValidationFailures = new DefaultValidationFailureCollection(validationFailures);
        }

        public IValidationFailureCollection ValidationFailures { get; }

        public object Model { get; }
    }
}
