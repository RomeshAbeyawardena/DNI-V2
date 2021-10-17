using DNI.Modules.Shared.Abstractions;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleStatus : IModuleStatus
    {
        public bool IsRunning { get; set; }
    }
}
