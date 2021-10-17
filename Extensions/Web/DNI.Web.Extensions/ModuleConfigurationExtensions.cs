using DNI.Modules.Shared.Abstractions;
using DNI.Web.Core.Defaults.Builders;
using DNI.Web.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using DNI.Modules.Extensions;
using System.Reflection;

namespace DNI.Web.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureWebModule(this IModuleConfiguration moduleConfiguration, 
            Assembly hostAssembly,
            Action<IWebModuleOptionsBuilder> buildAction)
        {
            IWebModuleOptionsBuilder webModuleOptionsBuilder = new DefaultWebModuleOptionsBuilder();
            buildAction?.Invoke(webModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(webModuleOptionsBuilder.Build(hostAssembly));
            return moduleConfiguration;
        }
    }
}
