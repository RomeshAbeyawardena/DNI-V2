using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class RuntimeBindingAttribute : Attribute
    {
        public RuntimeBindingAttribute(bool invokeAtRuntime)
        {
            InvokeAtRunTime = invokeAtRuntime;
        }

        // This is a named argument
        public bool InvokeAtRunTime { get; }
    }
}
