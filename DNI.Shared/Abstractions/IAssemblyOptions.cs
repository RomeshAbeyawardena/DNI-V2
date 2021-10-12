namespace DNI.Shared.Abstractions
{
    public interface IAssemblyOptions
    {
        bool Discoverable { get; set; }
        bool Injectable { get; set; }
        bool OnStartup { get; set; }
    }
}
