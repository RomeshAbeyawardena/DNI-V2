using System;

namespace DNI.Modules.Shared
{
    public class ModuleEventArgs
    {
        public ModuleEventArgs(object moduleInstance, bool isRunning)
        {
            ModuleInstance = moduleInstance;
            IsRunning = isRunning;
        }

        public object ModuleInstance { get; }
        public bool IsRunning { get; set; }
    }
}
