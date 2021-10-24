using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, IResponse<TModel>>
        where TRequest : Abstractions.IRequest<TModel>
    {
        protected IModelEncryptor Encryptor { get; }
        protected IRepository<TModel> Repository { get; }

        public abstract Task<TModel> Get(TRequest request, CancellationToken cancellationToken);

        public EncryptedRepositoryRequestHandlerBase(
            IModelEncryptor encryptor,
            IRepository<TModel> modelRepository)
        {
            this.Encryptor = encryptor;
            this.Repository = modelRepository;
        }

        public virtual async Task<IResponse<TModel>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await Get(request, cancellationToken);

            if(result != null)
            {
                return Response.Success(Encryptor.Decrypt(result));
            }

            return default;
        }
    }
}
