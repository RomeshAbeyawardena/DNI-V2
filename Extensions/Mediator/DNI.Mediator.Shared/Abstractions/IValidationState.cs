using DNI.Shared.Abstractions;
using System.Collections.Generic;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IValidationState
    {
        IEnumerable<IValidationFailure> ValidationFailures { get; }
        void SetValidationErrors(IEnumerable<IValidationFailure> validationFailures);
    }
}
