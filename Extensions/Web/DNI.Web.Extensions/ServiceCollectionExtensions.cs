using DNI.Web.Core.Defaults.Builders;
using DNI.Web.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection ConfigureWebModule(this IServiceCollection services, 
            Action<IWebModuleOptionsBuilder> buildAction)
        {
            IWebModuleOptionsBuilder webModuleOptionsBuilder = new DefaultWebModuleOptionsBuilder();
            buildAction?.Invoke(webModuleOptionsBuilder);
            return services.AddSingleton(webModuleOptionsBuilder.Build());
        }
    }
}
