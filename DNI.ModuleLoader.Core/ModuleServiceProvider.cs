using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.ModuleLoader.Core
{
    public class ModuleServiceProvider : IModuleServiceProvider
    {
        private readonly IServiceProvider baseServiceProvider;
        private readonly IServiceProvider serviceProvider;

        internal ModuleServiceProvider()
            : this(null, null)
        {

        }

        internal ModuleServiceProvider(
            IServiceProvider baseServiceProvider,
            IServiceProvider serviceProvider)
        {
            this.baseServiceProvider = baseServiceProvider;
            this.serviceProvider = serviceProvider;
        }

        public object GetService(Type serviceType)
        {
            return serviceProvider.GetService(serviceType) 
                ?? baseServiceProvider.GetService(serviceType);
        }
    }
}
