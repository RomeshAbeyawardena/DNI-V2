using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel> 
        : EncryptedRepositorySaveHandlerBase<TRequest, TModel, Guid>
        where TRequest : Abstractions.IRequest<Guid>
    {
        public EncryptedRepositorySaveHandlerBase(
           IServiceProvider serviceProvider,
           IModelEncryptor encryptor,
           IAsyncRepository<TModel> modelRepository)
            : base(serviceProvider, encryptor, modelRepository)
        {

        }
    }

    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel, TKey> : RepositorySaveHandlerBase<TRequest, TModel, TKey>, IRequestHandler<TRequest, IResponse<TKey>>
        where TRequest : Abstractions.IRequest<TKey>
    {
        protected IModelEncryptor Encryptor { get; }

        protected virtual Task<TModel> OnProcess(TModel model, CancellationToken cancellationToken)
        {
            return Task.FromResult(model);
        }

        public EncryptedRepositorySaveHandlerBase(
            IServiceProvider serviceProvider,
            IModelEncryptor encryptor,
            IAsyncRepository<TModel> modelRepository)
            : base(serviceProvider, modelRepository)
        {
            this.Encryptor = encryptor;
        }

        public override Task<TModel> Process(TModel model, CancellationToken cancellationToken)
        {
            return OnProcess(Encryptor.Encrypt(model), cancellationToken);
        }
    }
}
