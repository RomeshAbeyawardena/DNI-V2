using DNI.Shared.Base;
using DNI.Web.Shared.Abstractions;
using DNI.Web.Shared.Abstractions.Builders;
using System;
using System.Reflection;

namespace DNI.Web.Core.Defaults.Builders
{
    public class DefaultWebModuleOptionsBuilder : AssemblyOptionsBuilderBase, IWebModuleOptionsBuilder
    {
        private Action<Microsoft.AspNetCore.Mvc.MvcOptions> mvcOptions;
        private Action<Microsoft.AspNetCore.Hosting.IWebHostBuilder> configureAction;
        private bool useModuleAssemblies;
        
        public new IWebModuleOptions Build()
        {
            return new DefaultWebModuleOptions(configureAction, mvcOptions, useModuleAssemblies);
        }

        public IWebModuleOptionsBuilder ConfigureMvcOptions(Action<Microsoft.AspNetCore.Mvc.MvcOptions> mvcOptions)
        {
            this.mvcOptions = mvcOptions;
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

        public IWebModuleOptionsBuilder UseModuleAssemblies()
        {
            useModuleAssemblies = true;
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly<T>()
        {
            base.AddAssembly<T>();
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly(Type type)
        {
            base.AddAssembly(type);
            return this;
        }

        IWebModuleOptionsBuilder IWebModuleOptionsBuilder.AddAssembly(Assembly assembly)
        {
            base.AddAssembly(assembly);
            return this;
        }
    }
}
