using DNI.Modules.Shared.Builders;
using System;
using System.Reflection;

namespace DNI.FluentValidation.Shared.Abstractions.Builders
{
    public interface IFluentValidationModuleOptionsBuilder : IModuleOptionsAssemblyBuilder<IFluentValidationModuleOptions>
    {
        new IFluentValidationModuleOptionsBuilder AddAssembly(Assembly assembly);
        new IFluentValidationModuleOptionsBuilder AddAssembly(Type type);
        new IFluentValidationModuleOptionsBuilder AddAssembly<T>();

        IFluentValidationModuleOptionsBuilder AddModuleAssemblies();
    }
}
