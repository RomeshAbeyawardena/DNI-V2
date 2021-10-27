using DNI.Modules.Shared.Base.Buillders;
using DNI.Modules.Shared.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
