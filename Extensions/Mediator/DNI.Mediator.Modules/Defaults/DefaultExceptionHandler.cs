using DNI.Mediator.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Exceptions;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Modules.Defaults
{
    public class DefaultExceptionHandler<TRequest, TResponse, TException> 
        : IRequestExceptionHandler<TRequest, TResponse, TException>
        where TException : Exception
    {
        private readonly IModuleConfiguration moduleConfiguration;

        public DefaultExceptionHandler(IModuleConfiguration moduleConfiguration)
        {
            this.moduleConfiguration = moduleConfiguration;
        }

        public Task Handle(TRequest request, TException exception, 
            RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {
            IEnumerable<IValidationFailure> GetValidationFailures()
            {
                if (exception is ModelStateException modelStateException)
                {
                    return modelStateException.ValidationFailures;
                }

                return Array.Empty<IValidationFailure>();
            }

            var options = moduleConfiguration.GetOptions<MediatorModule, IMediatorModuleOptions>();

            List<Type> handledExceptionTypesList = new();
            handledExceptionTypesList.Add(typeof(ModelStateException));
            if(options.HandledExceptionTypes != null)
            {
                handledExceptionTypesList.AddRange(options.HandledExceptionTypes);
            }

            if(handledExceptionTypesList.Any(a => a == exception.GetType()))
            {
                
                var responseType = typeof(TResponse);
                var responseBaseType = typeof(IResponse);
                var responseBaseGenericType = typeof(ResponseBase<>);

                if (responseType.IsGenericType)
                {
                    var genericArguments = responseType.GetGenericArguments();
                    responseBaseGenericType = responseBaseGenericType.MakeGenericType(genericArguments);
                    state.SetHandled((TResponse)Activator.CreateInstance(responseBaseGenericType, Response.Failed(exception, GetValidationFailures())));
                }
                else
                {
                    state.SetHandled((TResponse)Response.Failed(exception, GetValidationFailures()));
                }
            }

            return Task.CompletedTask;
        }
    }
}
