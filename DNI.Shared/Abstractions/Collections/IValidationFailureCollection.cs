using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Collections
{
    public interface IValidationFailureCollection : IList<IValidationFailure>, IEnumerable<IValidationFailure>
    {
        
    }
}
