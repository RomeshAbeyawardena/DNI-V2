using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Abstractions;
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
    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel, TKey> : IRequestHandler<TRequest, TKey>
        where TRequest : IRequest<TKey>
    {
        protected IModelEncryptor Encryptor { get; }
        protected IAsyncRepository<TModel> Repository { get; }

        public EncryptedRepositorySaveHandlerBase(
            IModelEncryptor encryptor,
            IAsyncRepository<TModel> modelRepository)
        {
            this.Encryptor = encryptor;
            this.Repository = modelRepository;
        }

        protected abstract Task OnAdd(TRequest request, CancellationToken cancellationToken);
        protected abstract Task OnUpdate(TRequest request, CancellationToken cancellationToken);
        protected abstract Task<TKey> GetKey(TModel request, CancellationToken cancellationToken);
        protected abstract Task<TKey> SetKey(TModel request, CancellationToken cancellationToken);
        protected abstract TModel GetModel(TRequest request);

        public virtual async Task<TKey> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var model = GetModel(request);
            var encryptedCustomer = Encryptor.Encrypt(model);

            var key = await GetKey(model, cancellationToken);
            if (key.IsDefault())
            {
                key = await SetKey(model, cancellationToken);
                await OnAdd(request, cancellationToken);
                Repository.Add(encryptedCustomer);
            }
            else
            {
                await OnUpdate(request, cancellationToken);
                Repository.Update(encryptedCustomer);
            }

            await Repository.SaveChangesAsync(cancellationToken);
            return key;
        }
    }
}
