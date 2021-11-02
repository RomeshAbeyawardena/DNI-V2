namespace DNI.Mediator.Shared.Abstractions
{
    public interface IRequest<T> : MediatR.IRequest<IResponse<T>>
    {

    }
}
