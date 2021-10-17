using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static bool ApplyConfiguration(this IModuleConfiguration moduleConfiguration, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            bool hasChanged = false;

            void SetHasChangedFlag()
            {
                if(!hasChanged)
                    hasChanged = true;
            }
            var ct = moduleConfiguration.ModuleTypes.Count();
            var newModuleConfiguration = moduleConfigurationBuilder.Build(moduleConfiguration.ServiceProvider);
            var newct = moduleConfiguration.ModuleTypes.Count();

            hasChanged = ct != newct;

            foreach (var moduleType in newModuleConfiguration.ModuleTypes)
            {
                if (!moduleConfiguration.ModuleTypes.Contains(moduleType))
                {
                    moduleConfiguration.ModuleTypes = moduleConfiguration.ModuleTypes.Append(moduleType);
                    SetHasChangedFlag();
                }
            }

            foreach (var (t, o) in newModuleConfiguration.Options)
            {
                if (!moduleConfiguration.Options.ContainsKey(t))
                {
                    moduleConfiguration.Options.Add(t, o);
                    SetHasChangedFlag();
                }
            }

            return hasChanged;
        }
    }
}
