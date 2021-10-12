using DNI.Modules.Shared.Abstractions;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleOptions : IModuleOptions
    {
        public DefaultModuleOptions(IModuleAssemblyOptions moduleAssembliesOptions)
        {
            ModuleAssembliesOptions = moduleAssembliesOptions;
        }

        public IModuleAssemblyOptions ModuleAssembliesOptions { get; }
    }
}
