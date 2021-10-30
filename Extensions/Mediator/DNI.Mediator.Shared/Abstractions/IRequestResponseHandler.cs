using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IRequestResponseHandler<TRequest, TResult> : IRequestHandler<TRequest, IResponse<TResult>>
        where TRequest : IRequest<TResult>
    {
        
    }
}
