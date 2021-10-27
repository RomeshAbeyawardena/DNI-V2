using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using DNI.Shared.Exceptions;
using DNI.Shared.Extensions;
using DNI.Mediator.Shared.Extensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class RepositorySaveHandlerBase<TRequest, TModel, TKey> : InjectableServiceContainerBase, IRequestHandler<TRequest, IResponse<TKey>>
        where TRequest : Abstractions.IRequest<TKey>
    {
        protected IAsyncRepository<TModel> Repository { get; }

        public RepositorySaveHandlerBase(
            IServiceProvider serviceProvider)
            : base(serviceProvider)
        {
            this.Repository = this.GetService<IAsyncRepository<TModel>>();
        }


        public virtual Task<TModel> Process(TModel model, CancellationToken cancellationToken)
        {
            return Task.FromResult(model);
        }

        protected abstract Task<TKey> GetKey(TModel request, CancellationToken cancellationToken);
        protected abstract Task<TKey> SetKey(TModel request, CancellationToken cancellationToken);
        protected abstract TModel GetModel(TRequest request);

        /// <summary>
        /// Optional action to take when an insert/update completes
        /// </summary>
        /// <param name="request"></param>
        /// <param name="eventType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual Task OnAddUpdateSuccessful(TModel request, EventType eventType, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Optional action to take if an insert/update check fails
        /// </summary>
        /// <param name="request"></param>
        /// <param name="eventType"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected virtual Task OnAddUpdateFailure(TModel request, EventType eventType, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        /// <summary>
        /// Optional final check before an insert occurs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Determines whether the check has passed</returns>
        protected virtual Task<bool> OnAdd(TModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Optional final check before an update occurs
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Determines whether the check has passed</returns>
        protected virtual Task<bool> OnUpdate(TModel request, CancellationToken cancellationToken)
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Optional validation checks to apply before an insert/update operation can occur
        /// </summary>
        /// <param name="model">The model to be inserted/updated</param>
        /// <param name="cancellationToken"></param>
        /// <param name="validationFailures"></param>
        /// <returns>Determines whether validation has passed</returns>
        protected virtual Task<bool> ValidateModel(TModel model, CancellationToken cancellationToken,
            out IEnumerable<IValidationFailure> validationFailures)
        {
            validationFailures = Array.Empty<IValidationFailure>();
            return Task.FromResult(true);
        }

        public virtual async Task<IResponse<TKey>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            async Task AttemptHandle(TModel model, TModel processedModel,
                Func<TModel, CancellationToken, Task<bool>> conditionalAction, Action<TModel> action, EventType eventType)
            {
                if (await conditionalAction(model, cancellationToken))
                {
                    action(model);
                    await OnAddUpdateSuccessful(model, eventType, cancellationToken);
                }
                else await OnAddUpdateFailure(model, eventType, cancellationToken);
            }

            var model = GetModel(request);

            if (!await ValidateModel(model, cancellationToken, out var validationFailures))
            {
                throw new ModelStateException(model, validationFailures);
            }

            var key = await GetKey(model, cancellationToken);
            var processedModel = await Process(model, cancellationToken);
            if (key.IsDefault())
            {
                key = await SetKey(model, cancellationToken);
                await AttemptHandle(model, processedModel, OnAdd, model => Repository.Add(model), EventType.Add);
            }
            else
            {
                await AttemptHandle(model, processedModel, OnUpdate, model => Repository.Update(model), EventType.Update);
            }

            await Repository.SaveChangesAsync(cancellationToken);
            return Response.Success(key);
        }
    }
}