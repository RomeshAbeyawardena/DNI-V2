using DNI.Shared.Abstractions;

namespace DNI.Shared.Defaults
{
    public class DefaultAssemblyOptions : IAssemblyOptions
    {
        public bool Discoverable { get; set; }
        public bool Injectable { get; set; }
        public bool OnStartup { get; set; }
    }
}
