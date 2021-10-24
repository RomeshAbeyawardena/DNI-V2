using DNI.Encryption.Shared.Abstractions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestListHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, IResponse<IEnumerable<TModel>>>
        where TRequest : Abstractions.IRequest<IEnumerable<TModel>>
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

        public virtual async Task<IResponse<IEnumerable<TModel>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var results = await Get(request, cancellationToken);

            if (results != null && results.Any())
            {
                return Response.Success(results.Select(foundCustomer => Encryptor.Decrypt(foundCustomer)));
            }

            return Response.Success<IEnumerable<TModel>>(Array.Empty<TModel>());
        }
    }
}
