using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestListHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, IEnumerable<TModel>>
        where TRequest : IRequest<IEnumerable<TModel>>
    {
        protected IModelEncryptor Encryptor { get; }
        protected IRepository<TModel> Repository { get; }

        public abstract Task<IEnumerable<TModel>> Get(TRequest request, CancellationToken cancellationToken);

        public EncryptedRepositoryRequestListHandlerBase(
            IModelEncryptor encryptor,
            IRepository<TModel> modelRepository)
        {
            this.Encryptor = encryptor;
            this.Repository = modelRepository;
        }

        public virtual async Task<IEnumerable<TModel>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var results = await Get(request, cancellationToken);

            if (results != null && results.Any())
            {
                return results.Select(foundCustomer => Encryptor.Decrypt(foundCustomer));
            }

            return Array.Empty<TModel>();
        }
    }
}
