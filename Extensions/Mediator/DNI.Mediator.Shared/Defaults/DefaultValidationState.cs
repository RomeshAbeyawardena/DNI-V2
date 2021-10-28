using DNI.Mediator.Shared.Abstractions;
using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Defaults
{
    public class DefaultValidationState : IValidationState
    {
        public IEnumerable<IValidationFailure> ValidationFailures { get; private set; }

        public DefaultValidationState()
        {
            ValidationFailures = Array.Empty<IValidationFailure>();
        }

        public void SetValidationErrors(IEnumerable<IValidationFailure> validationFailures)
        {
            ValidationFailures = validationFailures;
        }
    }
}
