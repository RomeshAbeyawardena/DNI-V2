using DNI.Shared.Abstractions;
using DNI.Shared.Abstractions.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Base
{
    public abstract class ServiceBase : IService
    {
        public ServiceBase(IServiceConfig serviceConfig)
        {
            ServiceConfig = serviceConfig;
        }

        public IServiceConfig ServiceConfig { get; }
    }
}
