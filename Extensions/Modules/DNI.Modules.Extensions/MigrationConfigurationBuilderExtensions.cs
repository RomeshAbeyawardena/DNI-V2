using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class MigrationConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder AddModule<T>(this IModuleConfigurationBuilder builder, Action<IModuleConfiguration> configure = null)
        {
            return builder.AddModule(typeof(T), configure);
        }
    }
}
