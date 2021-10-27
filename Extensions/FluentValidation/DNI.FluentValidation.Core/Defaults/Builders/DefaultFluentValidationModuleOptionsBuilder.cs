using DNI.Modules.Shared.Base.Buillders;
using DNI.FluentValidation.Shared.Abstractions;
using DNI.FluentValidation.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DNI.Modules.Shared.Base.Builders;
using System.Reflection;

namespace DNI.FluentValidation.Core.Defaults.Builders
{
    public class DefaultFluentValidationModuleOptionsBuilder : ModuleOptionsAssemblyBuilderBase<IFluentValidationModuleOptions>, IFluentValidationModuleOptionsBuilder
    {
        private bool useModuleAssemblies = false;

        public override IFluentValidationModuleOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            return new DefaultFluentValidationModuleOptions { Assemblies = builtAssemblies, UseModuleAssemblies = useModuleAssemblies };
        }

        public new IFluentValidationModuleOptionsBuilder AddAssembly(Assembly assembly)
        {
            AddAssembly(assembly);
            return this;
        }

        public new IFluentValidationModuleOptionsBuilder AddAssembly(Type type)
        {
            return AddAssembly(type.Assembly);
        }

        public new IFluentValidationModuleOptionsBuilder AddAssembly<T>()
        {
            return AddAssembly(typeof(T));
        }

        public IFluentValidationModuleOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }
    }
}
