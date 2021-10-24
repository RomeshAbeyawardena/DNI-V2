using DNI.Shared.Abstractions.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
