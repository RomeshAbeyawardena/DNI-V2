using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ModuleAssemblyOptionsExtensions
    {
        public static IModuleAssemblyOptions AddAssembly(this IModuleAssemblyOptions options, Assembly assembly, Action<IAssemblyOptions> assemblyOptions)
        {
            var opt = new DefaultAssemblyOptions();
            assemblyOptions?.Invoke(opt);
            return options.AddAssembly(assembly, opt);
        }
    }
}
