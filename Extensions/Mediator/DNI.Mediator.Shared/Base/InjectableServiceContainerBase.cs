using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public class InjectableServiceContainerBase
    {
        private readonly IServiceProvider serviceProvider;

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
