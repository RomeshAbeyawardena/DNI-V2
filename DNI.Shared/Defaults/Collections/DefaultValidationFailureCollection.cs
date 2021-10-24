using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Collections;
using DNI.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
