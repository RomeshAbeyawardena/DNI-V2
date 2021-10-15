using DNI.Data.Extensions;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Modules
{
    public static class ModuleConfigurationBuilderExtensions
    {
        public static IModuleConfigurationBuilder ConfigureDbContextModule(this IModuleConfigurationBuilder builder, Action<IDbContextModuleOptionsBuilder> configure)
        {
            return builder.AddModule(typeof(DbContextModule), configuration => configuration.ConfigureDbContextModule(configure));
        }
    }
}
