﻿using DNI.Modules.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleConfigurationBuilder : IModuleConfigurationBuilder
    {
        private readonly IServiceCollection services;
        private readonly DefaultModuleAssemblyOptions moduleAssemblyOptions;
        public DefaultModuleConfigurationBuilder(IServiceCollection services)
        {
            moduleAssemblyOptions = new DefaultModuleAssemblyOptions();
            this.services = services;
        }

        public IModuleStartup Build()
        {
            services.AddSingleton<IDictionary<Assembly, IAssemblyOptions>>(moduleAssemblyOptions);
            return new DefaultModuleStartup(services, new DefaultModuleRunner(services.BuildServiceProvider(),
                new DefaultModuleOptions(moduleAssemblyOptions)));
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