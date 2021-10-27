using DNI.Mediator.Shared.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Extensions
{
    public static class InjectableServiceContainerBaseExtensions
    {
        public static T GetService<T>(this InjectableServiceContainerBase injectableServiceContainer)
        {
            return (T)injectableServiceContainer.GetService(typeof(T));
        }
    }
}
