using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using DNI.Shared.Exceptions;
using DNI.Shared.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel> 
        : EncryptedRepositorySaveHandlerBase<TRequest, TModel, Guid>
        where TRequest : Abstractions.IRequest<Guid>
    {
        public EncryptedRepositorySaveHandlerBase(
           IModelEncryptor encryptor,
           IAsyncRepository<TModel> modelRepository)
            : base(encryptor, modelRepository)
        {

        }
    }

    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel, TKey> : RepositorySaveHandlerBase<TRequest, TModel, TKey>, IRequestHandler<TRequest, IResponse<TKey>>
        where TRequest : Abstractions.IRequest<TKey>
    {
        protected IModelEncryptor Encryptor { get; }

        public EncryptedRepositorySaveHandlerBase(
            IModelEncryptor encryptor,
            IAsyncRepository<TModel> modelRepository)
            : base(modelRepository)
        {
            this.Encryptor = encryptor;
        }

        public override Task<TModel> Process(TModel model, CancellationToken cancellationToken)
        {
            return Task.FromResult(Encryptor.Encrypt(model));
        }
    }
}
