﻿using DNI.Data.Core.Defaults;
using DNI.Data.Shared.Abstractions.Builders;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Data.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureDbContextModule(
            this IModuleConfiguration moduleConfiguration,
            Action<IDbContextModuleOptionsBuilder> buildAction)
        {
            var defaultDbContextModuleOptionsBuilder = new DefaultDbContextModuleOptionsBuilder();
            buildAction(defaultDbContextModuleOptionsBuilder);

            var builtOptions = defaultDbContextModuleOptionsBuilder.Build();
            moduleConfiguration.ConfigureOptions(builtOptions);
            return moduleConfiguration;
        }

    }
}
