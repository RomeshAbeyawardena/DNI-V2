using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Enumerations
{
    [Flags]
    public enum AssemblyOptions
    {
        Discoverable = 0,
        Injectable = 2,
        OnStartup = 4
    }
}
