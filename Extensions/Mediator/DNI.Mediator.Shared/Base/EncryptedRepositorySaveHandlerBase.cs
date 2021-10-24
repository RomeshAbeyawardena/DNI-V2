using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
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
        where TRequest : IRequest<IResponse<Guid>>
    {
        public EncryptedRepositorySaveHandlerBase(
           IModelEncryptor encryptor,
           IAsyncRepository<TModel> modelRepository)
            : base(encryptor, modelRepository)
        {

        }
    }

    public abstract class EncryptedRepositorySaveHandlerBase<TRequest, TModel, TKey> : IRequestHandler<TRequest, IResponse<TKey>>
        where TRequest : IRequest<IResponse<TKey>>
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

        protected abstract Task<TKey> GetKey(TModel request, CancellationToken cancellationToken);
        protected abstract Task<TKey> SetKey(TModel request, CancellationToken cancellationToken);
        protected abstract TModel GetModel(TRequest request);

        protected virtual Task OnAdd(TModel request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task OnUpdate(TModel request, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        protected virtual Task<bool> ValidateModel(TModel model, CancellationToken cancellationToken, 
            out IEnumerable<IValidationFailure> validationFailures)
        {
            validationFailures = Array.Empty<IValidationFailure>();
            return Task.FromResult(true);
        }

        public virtual async Task<IResponse<TKey>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var model = GetModel(request);
            var encryptedCustomer = Encryptor.Encrypt(model);
            if(! await ValidateModel(encryptedCustomer, cancellationToken, out var validationFailures))
            {
                throw new ModelStateException(encryptedCustomer, validationFailures);
            }

            var key = await GetKey(encryptedCustomer, cancellationToken);
            if (key.IsDefault())
            {
                key = await SetKey(encryptedCustomer, cancellationToken);
                await OnAdd(encryptedCustomer, cancellationToken);
                Repository.Add(encryptedCustomer);
            }
            else
            {
                await OnUpdate(encryptedCustomer, cancellationToken);
                Repository.Update(encryptedCustomer);
            }

            await Repository.SaveChangesAsync(cancellationToken);
            return Response.Success(key);
        }
    }
}
