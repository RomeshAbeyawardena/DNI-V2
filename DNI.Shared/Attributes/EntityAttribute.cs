using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Attributes
{
    [System.AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = false)]
    public class EntityAttribute : Attribute
    {

        public EntityAttribute(bool enabled)
        {
            Enabled = enabled;
        }

        public bool Enabled { get; }

    }
}
