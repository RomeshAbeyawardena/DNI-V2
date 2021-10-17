﻿using DNI.Encryption.Shared.Abstractions;
using DNI.Shared.Abstractions;
using MediatR;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class EncryptedRepositoryRequestHandlerBase<TRequest, TModel> : IRequestHandler<TRequest, TModel>
        where TRequest : IRequest<TModel>
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

        public virtual async Task<TModel> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var result = await Get(request, cancellationToken);

            if(result != null)
            {
                return Encryptor.Decrypt(result);
            }

            return default;
        }
    }
}
