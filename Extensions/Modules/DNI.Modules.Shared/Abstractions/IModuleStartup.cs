namespace DNI.Modules.Shared.Abstractions
{
    public interface IModuleStartup : IModule
    {
        IModuleConfiguration Configuration { get; }
    }
}
