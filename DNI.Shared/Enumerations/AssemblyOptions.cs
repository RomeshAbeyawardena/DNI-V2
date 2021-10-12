using System;

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
