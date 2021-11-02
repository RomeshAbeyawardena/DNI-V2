using System;
using System.Collections.Generic;

namespace DNI.Web.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ClientControllerAllowedHeadersAttribute : Attribute
    {
        public const string Any = "AnyHeader";
        
        public ClientControllerAllowedHeadersAttribute(params string[] headers)
        {
            Headers = headers;
        }

        public IEnumerable<string> Headers { get; }
    }
}
