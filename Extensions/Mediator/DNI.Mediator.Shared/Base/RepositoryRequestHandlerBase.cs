using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class RepositoryRequestHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, IResponse<TModel>>
        where TRequest : Abstractions.IRequest<TModel>
    {
        protected IRepository<TModel> Repository { get; }

        public abstract Task<TModel> Get(TRequest request, CancellationToken cancellationToken);
        public virtual Task<TModel> Process(TModel model, CancellationToken cancellationToken)
        {
            return Task.FromResult(model);
        }

        public RepositoryRequestHandlerBase(
            IRepository<TModel> modelRepository)
        {
            this.Repository = modelRepository;
        }

        public virtual async Task<IResponse<TModel>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await Get(request, cancellationToken);

            if (result != null)
            {
                return Response.Success(await Process(result, cancellationToken));
            }

            return default;
        }
    }
}
