using DNI.Modules.Shared.Abstractions;
using System;

namespace DNI.Modules.Core.Defaults
{
    public class DefaultModuleServiceProvider : IModuleServiceProvider
    {
        private readonly IServiceProvider defaultServiceProvider;
        private readonly IServiceProvider moduleServiceProvider;

        public DefaultModuleServiceProvider(IServiceProvider defaultServiceProvider, IServiceProvider moduleServiceProvider)
        {
            this.defaultServiceProvider = defaultServiceProvider;
            this.moduleServiceProvider = moduleServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            return moduleServiceProvider.GetService(serviceType)
                ?? defaultServiceProvider.GetService(serviceType);
        }
    }
}
