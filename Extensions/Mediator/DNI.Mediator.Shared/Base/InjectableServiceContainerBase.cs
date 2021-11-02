using System;

namespace DNI.Mediator.Shared.Base
{
    public class InjectableServiceContainerBase
    {
        protected readonly IServiceProvider serviceProvider;

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType);
        }

        public InjectableServiceContainerBase(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}
