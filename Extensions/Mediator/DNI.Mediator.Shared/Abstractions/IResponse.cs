using DNI.Shared.Abstractions.Collections;
using System;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IResponse<T> : IResponse
    {
        new T Result { get; }
    }

    public interface IResponse
    {
        object Result { get; }
        bool Succeeded { get; }
        Exception Exception { get; }
        IValidationFailureCollection ValidationFailures { get; }
    }
}
