using System;
using System.Collections.Generic;

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
