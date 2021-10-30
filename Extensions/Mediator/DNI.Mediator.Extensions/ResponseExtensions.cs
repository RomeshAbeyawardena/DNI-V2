using DNI.Mediator.Shared.Abstractions;
using DNI.Mediator.Shared.Defaults;
using DNI.Shared.Abstractions.Collections;
using DNI.Shared.Defaults.Collections;
using DNI.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Extensions
{
    public static class ResponseExtensions
    {
        public async static Task<IResponse<TResult>> Response<TRequest, TResult>(
            this IRequestResponseHandler<TRequest, TResult> requestHandler, 
            Func<TResult, bool> validationPassed, Func<IValidationFailureCollection, Task<TResult>> @do)
            where TRequest : IRequest<TResult>
        {
            var validationState = new DefaultValidationState();
            var validationFailureCollection = new DefaultValidationFailureCollection();
            var result = await @do(validationFailureCollection);
            validationState.SetValidationErrors(validationFailureCollection);

            if (validationPassed(result))
                return Shared.Base.Response.Success(result);

            throw new ModelStateException(result, validationState.ValidationFailures);
        }
    }
}
