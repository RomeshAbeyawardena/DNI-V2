using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;

namespace DNI.Modules.Extensions
{
    public abstract class ModuleBase<TOptions>
        : ModuleBase
    {
        protected readonly IModuleConfiguration moduleConfiguration;

        protected TOptions Options => moduleConfiguration.GetOptions<TOptions>(ModuleDescriptor);

        public ModuleBase(IModuleConfiguration moduleConfiguration)
        {
            this.moduleConfiguration = moduleConfiguration;
        }
    }
}
