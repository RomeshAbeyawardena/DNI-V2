using DNI.Modules.Shared.Base.Builders;
using DNI.Web.Shared.Abstractions;
using DNI.Web.Shared.Abstractions.Builders;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DNI.Web.Core.Defaults.Builders
{
    public class DefaultWebModuleOptionsBuilder : ModuleOptionsAssemblyBuilderBase<IWebModuleOptions>, IWebModuleOptionsBuilder
    {
        private readonly Assembly hostAssembly;
        private Action<IMvcBuilder> configureMvcOptions;
        private Action<Microsoft.AspNetCore.Hosting.IWebHostBuilder> configureAction;
        private Action<IServiceCollection> configureServiceAction; 
        private bool useModuleAssemblies;

        public DefaultWebModuleOptionsBuilder(Assembly hostAssembly)
        {
            this.hostAssembly = hostAssembly;
        }

        public override IWebModuleOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            var opts = new DefaultWebModuleOptions(configureAction, configureMvcOptions, configureServiceAction, useModuleAssemblies, hostAssembly);
            opts.AddRange(builtAssemblies);
            return opts;
        }

        public IWebModuleOptionsBuilder ConfigureMvcOptions(Action<IMvcBuilder> mvcOptions)
        {
            this.configureMvcOptions = mvcOptions;
            return this;
        }

        public IWebModuleOptionsBuilder ConfigureWebHost(Action<Microsoft.AspNetCore.Hosting.IWebHostBuilder> configureAction)
        {
            this.configureAction = configureAction;
            return this;
        }

        public IWebModuleOptionsBuilder UseDefaultMvcOptions()
        {
            return this;
        }

        public IWebModuleOptionsBuilder AddModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly<T>()
        {
            AddAssembly<T>();
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly(Type type)
        {
            AddAssembly(type);
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly(Assembly assembly)
        {
            AddAssembly(assembly);
            return this;
        }

        public IWebModuleOptionsBuilder ConfigureServices(Action<IServiceCollection> services)
        {
            configureServiceAction = services;
            return this;
        }
    }
}
