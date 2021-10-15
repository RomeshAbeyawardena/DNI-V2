using DNI.Modules.Shared.Abstractions;
using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleConfigurationBuilder : IModuleConfigurationBuilder
    {
        private readonly IModuleAssemblyResolverOptions moduleAssemblyResolverOptions;
        private readonly IServiceCollection services;
        private readonly DefaultModuleAssemblyOptions moduleAssemblyOptions;
        public DefaultModuleConfigurationBuilder(IServiceCollection services)
        {
            moduleAssemblyResolverOptions = new DefaultModuleAssemblyResolverOptions();
            moduleAssemblyOptions = new DefaultModuleAssemblyOptions();
            this.services = services;
        }

        public IModuleConfigurationBuilder ConfigureResolverOptions(Action<IModuleAssemblyResolverOptions> configureResolver)
        {
            configureResolver?.Invoke(moduleAssemblyResolverOptions);
            moduleAssemblyOptions.ConfigureResolver(moduleAssemblyResolverOptions);
            return this;
        }

        public IModuleStartup Build()
        {
            services.AddSingleton<IDictionary<Assembly, IAssemblyOptions>>(moduleAssemblyOptions);
            return new DefaultModuleStartup(services, new DefaultModuleRunner(services, new DefaultModuleOptions(moduleAssemblyOptions)));
        }

        public IModuleStartup Build(Action<IModuleConfigurationBuilder> configure)
        {
            configure?.Invoke(this);
            return Build();
        }

        public IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyOptions> configure)
        {
            configure(moduleAssemblyOptions);
            return this;
        }

        public IModuleConfigurationBuilder ConfigureAssemblies(Action<IModuleAssemblyLocatorOptions> configure)
        {
            configure(moduleAssemblyOptions);
            return this;
        }
    }
}
