using System;

namespace DNI.Web.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class ClientControllerMaxAgeAttribute : Attribute
    {
        public ClientControllerMaxAgeAttribute(string timeSpanInSeconds)
        {
            TimeSpanInSeconds = timeSpanInSeconds;
        }

        public string TimeSpanInSeconds { get; }
    }
}
