using DNI.Web.Shared.Abstractions.Providers;
using DNI.Web.Shared.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

namespace DNI.Web.Shared.Base
{
    [Route("{controller}/{action}"), ClientController]
    public abstract class ControllerBase : Microsoft.AspNetCore.Mvc.ControllerBase
    {
        private T GetControllerService<T>() => HttpContext.RequestServices.GetService<T>();
        private IWebServiceProvider WebServiceProvider => GetControllerService<IWebServiceProvider>();

        private Type Type => this.GetType();

        private bool IsClientController()
        {
            var clientControllerAttribute = Type.GetCustomAttribute<ClientControllerAttribute>();

            if(clientControllerAttribute == null)
            {
                return false;
            }

            return true;
        }

        [NonAction]
        private string GetAllowedOrigins()
        {
            var origins = Type
                .GetCustomAttributes<ClientControllerAllowedOriginsAttribute>()
                .SelectMany(t => t.Origins);

            if (origins.Any())
            {
                if(origins.Contains(ClientControllerAllowedOriginsAttribute.Any))
                {
                    return "*";
                }

                return string.Join(",", origins);
            }

            return string.Empty;
        }

        [NonAction]
        private string GetAllowedHeaders()
        {
            var headers = Type
                .GetCustomAttributes<ClientControllerAllowedHeadersAttribute>()
                .SelectMany(t => t.Headers);

            if (headers.Any())
            {
                if (headers.Contains(ClientControllerAllowedHeadersAttribute.Any))
                {
                    return "*";
                }

                return string.Join(",", headers);
            }

            return string.Empty;
        }

        [NonAction]
        private string GetAllowedMethods()
        {
            var methods = Type
                .GetCustomAttributes<ClientControllerAllowedMethodsAttribute>()
                .SelectMany(t => t.Methods);

            if (methods.Any())
            {
                if (methods.Contains(ClientControllerAllowedMethodsAttribute.Any))
                {
                    return "*";
                }

                return string.Join(",", methods);
            }

            return string.Empty;
        }

        [NonAction]
        private string GetMaxAge()
        {
            var maxAgeAttribute = Type.GetCustomAttribute<ClientControllerMaxAgeAttribute>();

            if (maxAgeAttribute == null)
            {
                return "86400";
            }

            return maxAgeAttribute.TimeSpanInSeconds;
        }

        [NonAction]
        private void SetHeaders()
        {
            if (IsClientController())
            {
                var headers = Response.Headers;
                headers.Add("Access-Control-Allow-Origin", GetAllowedOrigins());
                headers.Add("Access-Control-Allow-Methods", GetAllowedMethods());
                headers.Add("Access-Control-Allow-Headers", GetAllowedHeaders());
                headers.Add("Access-Control-Max-Age", GetMaxAge());
            }

        }

        public override OkResult Ok()
        {
            SetHeaders();   
            return base.Ok();
        }

        public override OkObjectResult Ok([ActionResultObjectValue] object value)
        {
            SetHeaders();
            return base.Ok(value);
        }

        public T GetService<T>() => WebServiceProvider.GetService<T>();
    }
}
