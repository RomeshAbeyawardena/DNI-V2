using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class ClientControllerMaxAgeAttribute : Attribute
    {
        public ClientControllerMaxAgeAttribute(string timeSpanInSeconds)
        {
            TimeSpanInSeconds = timeSpanInSeconds;
        }

        public string TimeSpanInSeconds { get; }
    }
}
