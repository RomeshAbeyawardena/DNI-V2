using System;
using System.Collections.Generic;

namespace DNI.Web.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public sealed class ClientControllerAllowedMethodsAttribute : Attribute
    {
        public const string Any = "AnyMethod";

        public ClientControllerAllowedMethodsAttribute(params string[] methods)
        {
            Methods = methods;
        }

        public IEnumerable<string> Methods { get; }
    }
}
