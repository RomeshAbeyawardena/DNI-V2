using DNI.Mediator.Shared.Abstractions;
using DNI.Mediator.Shared.Base;
using DNI.Shared.Abstractions;
using DNI.Shared.Defaults.Collections;
using FluentValidation;
using FluentValidation.Results;
using DNI.Core.Defaults;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationFailure = FluentValidation.Results.ValidationFailure;
namespace DNI.FluentValidation.Extensions
{
    public static class RepositorySaveHandlerBaseExtensions
    {
        public static Task<ValidationResult> Validate<TRequest, TModel, TKey>(this RepositorySaveHandlerBase<TRequest, TModel, TKey> handler, TModel model, CancellationToken cancellationToken)
            where TRequest : IRequest<TKey>
        {
            var validator = (IValidator)handler.GetService(typeof(IValidator<TModel>));

            if (validator != null)
            {
                return validator.ValidateAsync(new ValidationContext<TModel>(model), cancellationToken);
            }

            return Task.FromResult(new ValidationResult());
        }

        public static IEnumerable<IValidationFailure> GetValidationFailures<TRequest, TModel, TKey>(this RepositorySaveHandlerBase<TRequest, TModel, TKey> handler, IEnumerable<ValidationFailure> validationFailures)
            where TRequest : IRequest<TKey>
        {
            return validationFailures.Select(a => DNI.Core.Defaults.ValidationFailure.Create(a.AttemptedValue, new System.Exception(a.ErrorMessage), a.PropertyName));
        }
    }
}
