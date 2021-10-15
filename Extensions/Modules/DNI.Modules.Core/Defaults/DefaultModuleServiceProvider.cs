using System;

namespace DNI.Modules.Core.Defaults
{
    internal class DefaultModuleServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider parentServiceProvider;
        private readonly IServiceProvider serviceProvider;

        public DefaultModuleServiceProvider(
            IServiceProvider parentServiceProvider,
            IServiceProvider serviceProvider)
        {
            this.parentServiceProvider = parentServiceProvider;
            this.serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return parentServiceProvider.GetService(serviceType) 
                ?? serviceProvider.GetService(serviceType);
        }
    }
}
