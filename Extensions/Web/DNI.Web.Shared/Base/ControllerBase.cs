using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Web.Shared.Base
{
    [Route("{controller}/{action}")]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        public T GetService<T>() => HttpContext.RequestServices.GetService<T>();
    }
}
