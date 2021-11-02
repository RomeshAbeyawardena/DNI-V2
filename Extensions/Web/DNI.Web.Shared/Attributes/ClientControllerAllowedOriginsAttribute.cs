using System;
using System.Collections.Generic;

namespace DNI.Web.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class ClientControllerAllowedOriginsAttribute : Attribute
    {
        public const string Any = "AnyOrigin";

        public ClientControllerAllowedOriginsAttribute(params string[] origins)
        {
            Origins = origins;
        }

        public IEnumerable<string> Origins { get; }
    }
}
