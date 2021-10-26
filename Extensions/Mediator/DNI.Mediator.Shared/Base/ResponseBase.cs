using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Collections;
using DNI.Shared.Defaults.Collections;
using System;
using System.Collections.Generic;

namespace DNI.Mediator.Shared.Base
{
    public static class Response
    {
        public static IResponse Success(object result)
        {
            return new ResponseBase(result);
        }

        public static IResponse<T> Success<T>(T result)
        {
            return new ResponseBase<T>(result);
        }

        public static IResponse Failed(Exception exception, IEnumerable<IValidationFailure> validationFailures = default)
        {
            return new ResponseBase(exception, validationFailures);
        }

        public static IResponse Failed(IEnumerable<IValidationFailure> validationFailures)
        {
            return new ResponseBase(validationFailures);
        }
    }

    internal class ResponseBase<T> : ResponseBase, IResponse<T>
    {
        public ResponseBase(IResponse response)
            : base(response)
        {

        }

        public ResponseBase(T result)
            : base(result)
        {

        }

        public ResponseBase(Exception exception)
            : base(exception)
        {

        }

        public ResponseBase(IEnumerable<IValidationFailure> validateFailures)
            : base(validateFailures)
        {

        }

        T IResponse<T>.Result => (T)Result;
    }

    internal class ResponseBase : IResponse
    {
        public ResponseBase(IResponse response)
        {
            Result = response.Result;
            Succeeded = response.Succeeded;
            Exception = response.Exception;
            ValidationFailures = response.ValidationFailures;
        }

        public ResponseBase(object result)
        {
            Result = result;
            Succeeded = result != null;
        }

        public ResponseBase(Exception exception, IEnumerable<IValidationFailure> validationFailures = default)
            : this(validationFailures)
        {
            Exception = exception;
            Succeeded = false;
        }

        public ResponseBase(IEnumerable<IValidationFailure> validateFailures)
        {
            ValidationFailures = new DefaultValidationFailureCollection(validateFailures);
        }

        public object Result { get; }

        public bool Succeeded { get; }

        public Exception Exception { get; }

        public IValidationFailureCollection ValidationFailures { get; }
    }
}
