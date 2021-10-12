using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class RequiresDependenciesAttribute : Attribute
    {
        public RequiresDependenciesAttribute(params Type[] requiredTypes)
        {
            RequiredTypes = requiredTypes;
        }

        public IEnumerable<Type> RequiredTypes { get; }
    }
}
