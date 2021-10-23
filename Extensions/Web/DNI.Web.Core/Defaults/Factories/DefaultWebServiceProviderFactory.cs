﻿using DNI.Web.Core.Defaults.Builders;
using DNI.Web.Core.Defaults.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Core.Defaults.Factories
{
    public class DefaultWebServiceProviderFactory : IServiceProviderFactory<DefaultWebServiceProviderBuilder>
    {
        private readonly IEnumerable<ServiceDescriptor> serviceDescriptors;

        public DefaultWebServiceProviderFactory(IEnumerable<ServiceDescriptor> serviceDescriptors)
        {
            this.serviceDescriptors = serviceDescriptors;
        }

        public DefaultWebServiceProviderBuilder CreateBuilder(IServiceCollection services)
        {
            return new DefaultWebServiceProviderBuilder(services, serviceDescriptors);
        }

        public IServiceProvider CreateServiceProvider(DefaultWebServiceProviderBuilder containerBuilder)
        {
            return containerBuilder.Build();
        }
    }
}
