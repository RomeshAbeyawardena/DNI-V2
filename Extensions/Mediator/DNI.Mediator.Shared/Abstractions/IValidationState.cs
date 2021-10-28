using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IValidationState
    {
        IEnumerable<IValidationFailure> ValidationFailures { get; }
        void SetValidationErrors(IEnumerable<IValidationFailure> validationFailures);
    }
}
