using DNI.Modules.Shared.Abstractions;
using DNI.Web.Core.Defaults.Builders;
using DNI.Web.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using DNI.Modules.Extensions;
namespace DNI.Web.Extensions
{
    public static class ModuleConfigurationExtensions
    {
        public static IModuleConfiguration ConfigureWebModule(this IModuleConfiguration moduleConfiguration, 
            Action<IWebModuleOptionsBuilder> buildAction)
        {
            IWebModuleOptionsBuilder webModuleOptionsBuilder = new DefaultWebModuleOptionsBuilder();
            buildAction?.Invoke(webModuleOptionsBuilder);
            moduleConfiguration.ConfigureOptions(webModuleOptionsBuilder.Build());
            return moduleConfiguration;
        }
    }
}
