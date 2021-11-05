using DNI.Cms.Shared.Abstractions;
using DNI.Cms.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Cms.Web.Common.ApplicationBuilder;

namespace DNI.Cms.Core.Defaults.Builders
{
    public class DefaultCmsModuleOptionsBuilder : ModuleOptionsAssemblyBuilderBase<ICmsModuleOptions>, ICmsModuleOptionsBuilder
    {
        private Type parentType;
        private bool enableWebsite;
        private Action<IUmbracoApplicationBuilderContext> configureMiddleware;
        private Action<IUmbracoEndpointBuilderContext> configureEndpoints;

        public override ICmsModuleOptions BuildOptions(IEnumerable<Assembly> builtAssemblies)
        {
            return new DefaultCmsModuleOptions { 
                EnableWebsite = enableWebsite,
                ParentType = parentType,
                ConfigureEndpoints = configureEndpoints,
                ConfigureMiddleware = configureMiddleware
            };
        }

        public ICmsModuleOptionsBuilder ConfigureEndpoints(Action<IUmbracoEndpointBuilderContext> configureEndpoints)
        {
            this.configureEndpoints = configureEndpoints;
            return this;
        }

        public ICmsModuleOptionsBuilder ConfigureMiddleware(Action<IUmbracoApplicationBuilderContext> configureMiddleware)
        {
            this.configureMiddleware = configureMiddleware;
            return this;
        }

        public ICmsModuleOptionsBuilder EnableWebsite()
        {
            enableWebsite = true;
            return this;
        }

        public ICmsModuleOptionsBuilder SetParentType(Type parentType)
        {
            this.parentType = parentType;
            return this;
        }

        ICmsModuleOptionsBuilder ICmsModuleOptionsBuilder.AddAssembly<T>()
        {
            base.AddAssembly(typeof(T));
            return this;
        }

        ICmsModuleOptionsBuilder ICmsModuleOptionsBuilder.AddAssembly(Type type)
        {
            base.AddAssembly(type);
            return this;
        }

        ICmsModuleOptionsBuilder ICmsModuleOptionsBuilder.AddAssembly(Assembly assembly)
        {
            base.AddAssembly(assembly);
            return this;
        }
    }
}
