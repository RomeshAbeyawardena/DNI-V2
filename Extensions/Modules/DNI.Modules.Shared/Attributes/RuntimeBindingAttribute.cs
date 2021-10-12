using System;

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
