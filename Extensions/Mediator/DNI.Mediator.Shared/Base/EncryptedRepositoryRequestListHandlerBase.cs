using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestListHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, IEnumerable<TModel>>
        where TRequest : IRequest<IEnumerable<TModel>>
    {
        protected IModelEncryptor Encryptor { get; }
        protected IRepository<TModel> Repository { get; }

        public EncryptedRepositoryRequestListHandlerBase(
            IModelEncryptor encryptor,
            IRepository<TModel> modelRepository)
        {
            this.Encryptor = encryptor;
            this.Repository = modelRepository;
        }

        public abstract Task<IEnumerable<TModel>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
