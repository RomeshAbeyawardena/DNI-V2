using DNI.Modules.Core.Defaults;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddModules(this IServiceCollection services, Action<IModuleConfigurationBuilder> configureModules)
        {
            IModuleConfiguration ConfigureModuleConfiguration()
            {
                DefaultModuleConfigurationBuilder builder = new();
                configureModules(builder);
                return  builder.Build(new FakeServiceProvider());
            }

            IModuleRunner ConfigureModuleRunner(IServiceProvider serviceProvider)
            {
                var moduleConfiguration = serviceProvider.GetRequiredService<IModuleConfiguration>();
                return moduleConfiguration.ConfigureRunner(serviceProvider);
            }

            return services
                .AddSingleton(ConfigureModuleConfiguration())
                .AddSingleton(ConfigureModuleRunner)
                .AddSingleton<IModuleStartup, DefaultModuleStartup>();
        }
    }
}
