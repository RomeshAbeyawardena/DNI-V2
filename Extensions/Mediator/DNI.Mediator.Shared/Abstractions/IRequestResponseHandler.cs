using MediatR;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IRequestResponseHandler<TRequest, TResult> : IRequestHandler<TRequest, IResponse<TResult>>
        where TRequest : IRequest<TResult>
    {
        
    }
}
