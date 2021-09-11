using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions.Services
{
    public interface IService
    {
        IServiceConfig ServiceConfig { get; }
    }
}
