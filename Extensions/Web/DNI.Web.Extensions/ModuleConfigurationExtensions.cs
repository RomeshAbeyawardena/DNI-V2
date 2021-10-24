using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Web.Core.Defaults.Builders;
using DNI.Web.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace DNI.Web.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureWebModule(this IModuleConfiguration moduleConfiguration,
            IModuleDescriptor moduleDescriptor,
            Assembly hostAssembly,
            Action<IWebModuleOptionsBuilder> buildAction)
        {
            IWebModuleOptionsBuilder webModuleOptionsBuilder = new DefaultWebModuleOptionsBuilder();
            buildAction?.Invoke(webModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(moduleDescriptor, webModuleOptionsBuilder.Build(hostAssembly));
            return moduleConfiguration;
        }
    }
}
