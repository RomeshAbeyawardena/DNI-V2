using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class RequiresAttribute : Attribute
    {
        public RequiresAttribute(params Type[] requiredTypes)
        {
            RequiredTypes = requiredTypes;
        }

        public IEnumerable<Type> RequiredTypes { get; }
    }
}
