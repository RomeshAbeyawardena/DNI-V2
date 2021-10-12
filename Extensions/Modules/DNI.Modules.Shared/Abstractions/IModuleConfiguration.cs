using DNI.Shared.Abstractions;

namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleConfiguration
    {
        bool Enabled { get; set; }
        string ModuleName { get; set; }
        string AssemblyName { get; set; }
        string FileName { get; set; }
        IAssemblyOptions Options { get; set; }
    }
}
