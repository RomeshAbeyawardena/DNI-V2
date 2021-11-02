using System.Collections.Generic;

namespace DNI.Shared.Abstractions.Collections
{
    public interface IValidationFailureCollection : IList<IValidationFailure>, IEnumerable<IValidationFailure>
    {

    }
}
