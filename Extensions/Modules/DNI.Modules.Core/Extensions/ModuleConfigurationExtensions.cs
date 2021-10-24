﻿using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System.Linq;

namespace DNI.Modules.Core.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static bool ApplyConfiguration(this IModuleConfiguration moduleConfiguration, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            var ct = moduleConfiguration.ModuleDescriptors.Count();
            moduleConfigurationBuilder.Build(moduleConfiguration.ServiceProvider);
            var newct = moduleConfiguration.ModuleDescriptors.Count();

            return ct != newct;
        }
    }
}
