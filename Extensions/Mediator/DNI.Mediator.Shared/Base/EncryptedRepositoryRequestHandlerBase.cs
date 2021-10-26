using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestHandlerBase<TRequest, TModel> : RepositoryRequestHandlerBase<TRequest, TModel>, IRequestHandler<TRequest, IResponse<TModel>>
        where TRequest : Abstractions.IRequest<TModel>
    {
        protected IModelEncryptor Encryptor { get; }

        public EncryptedRepositoryRequestHandlerBase(
            IModelEncryptor encryptor,
            IRepository<TModel> modelRepository)
            : base(modelRepository)
        {
            this.Encryptor = encryptor;
        }

        public override Task<TModel> Process(TModel model, CancellationToken cancellationToken)
        {
            return Task.FromResult(Encryptor.Decrypt(model));
        }
    }
}
