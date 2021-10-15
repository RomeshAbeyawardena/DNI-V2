using System;

namespace DNI.Modules.Core.Defaults
{
    public class ModuleServiceProvider : IServiceProvider
    {
        private readonly IServiceProvider parentServiceProvider;
        private readonly IServiceProvider serviceProvider;

        public ModuleServiceProvider(
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
