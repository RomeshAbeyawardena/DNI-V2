using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Extensions
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureDbModule(this IModuleConfigurationBuilder builder, Action<IDbContextModuleOptionsBuilder> configure)
        {
            return builder.ConfigureDbModule(configure);
        }
    }
}
