using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Collections;
using DNI.Shared.Base;
using System.Collections.Generic;

namespace DNI.Shared.Defaults.Collections
{
    public class DefaultValidationFailureCollection : CollectionBase<IValidationFailure>, IValidationFailureCollection
    {
        public DefaultValidationFailureCollection()
        {

        }

        public DefaultValidationFailureCollection(IEnumerable<IValidationFailure> validationFailures)
            : base(validationFailures)
        {

        }
    }
}
