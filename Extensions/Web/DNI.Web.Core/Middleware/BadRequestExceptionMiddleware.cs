using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Core.Middleware
{
    public class BadRequestExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public BadRequestExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            var exHandlerFeature = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exHandlerFeature.Error;
            await next(httpContext);
        }
    }
}
