using DNI.Mediator.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.FluentValidation.Extensions
{
    public static class RepositorySaveHandlerBaseExtensions
    {
        public static Task<ValidationResult> Validate<TRequest, TModel, TKey>(this RepositorySaveHandlerBase<TRequest, TModel, TKey> handler, TModel model, CancellationToken cancellationToken)
            where TRequest : IRequest<TKey>
        {
            var validator = (IValidator)handler.GetService(typeof(IValidator<TModel>));

            return validator.ValidateAsync(new ValidationContext<TModel>(model), cancellationToken);
        }
    }
}
