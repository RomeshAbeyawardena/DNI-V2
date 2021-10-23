using DNI.Web.Shared.Abstractions.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DNI.Web.Core.Defaults.Providers
{
    public class DefaultWebServiceProvider : IWebServiceProvider
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IServiceProvider otherServiceProvider;
        public DefaultWebServiceProvider(IServiceProvider serviceProvider,
            IServiceProvider otherServiceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.otherServiceProvider = otherServiceProvider;
        }

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType)
                ?? otherServiceProvider.GetRequiredService(serviceType);
        }
    }
}
