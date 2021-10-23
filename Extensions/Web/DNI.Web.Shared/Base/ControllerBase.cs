using DNI.Web.Shared.Abstractions.Providers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Web.Shared.Base
{
    [Route("{controller}/{action}")]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private T GetControllerService<T>() => HttpContext.RequestServices.GetService<T>();
        private IWebServiceProvider WebServiceProvider => GetControllerService<IWebServiceProvider>();


        public T GetService<T>() => WebServiceProvider.GetService<T>();
    }
}
