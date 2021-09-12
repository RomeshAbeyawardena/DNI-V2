using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Attributes
{
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public sealed class ServiceDiscoveryAttribute : Attribute
    {
        public ServiceDiscoveryAttribute(bool enableServiceDiscovery = true)
        {
            EnableServiceDiscovery = enableServiceDiscovery;
        }

        public bool EnableServiceDiscovery { get; }
    }
}
