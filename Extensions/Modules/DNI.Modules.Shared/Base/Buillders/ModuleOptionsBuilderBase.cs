using DNI.Modules.Shared.Builders;

namespace DNI.Modules.Shared.Base.Buillders
{
    public abstract class ModuleOptionsBuilderBase<TOptions> : IModuleOptionsBuilder<TOptions>
    {
        public abstract TOptions Build();
    }
}
