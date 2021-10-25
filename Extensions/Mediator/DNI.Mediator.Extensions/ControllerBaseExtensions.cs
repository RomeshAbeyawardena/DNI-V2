using DNI.Extensions;
using DNI.Mediator.Shared.Abstractions;
using DNI.Web.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ControllerBase = DNI.Web.Shared.Base.ControllerBase;

namespace DNI.Mediator.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static async Task<IActionResult> Process<T>(this ControllerBase controllerBase, Task<IResponse<T>> response)
        {
            return Process(controllerBase, await response);
        }

        public static async Task<IActionResult> Process(this ControllerBase controllerBase, Task<IResponse> response)
        {
            return Process(controllerBase, await response);
        }

        public static IActionResult Process(this ControllerBase controllerBase, IResponse response)
        {
            if (response.Succeeded)
            {
                return controllerBase.Ok(response.Result);
            }

            if(response.ValidationFailures != null 
                && response.ValidationFailures.Any())
            {
                response.ValidationFailures.ForEach(a => controllerBase.ModelState.AddModelError(a.Property.Name, a.Exception.Message));
                return controllerBase.BadRequest(controllerBase.ModelState);
            }

            return controllerBase.BadRequest(response.Exception.Message);
        }

        public static Task<TResponse> Send<TResponse>(this ControllerBase controllerBase, MediatR.IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            var mediator = controllerBase.GetService<IMediator>();

            return mediator.Send(request, cancellationToken);
        }

        public static Task Publish<TNotification>(this ControllerBase controllerBase, TNotification notification, CancellationToken cancellationToken)
            where TNotification : INotification
        {
            var mediator = controllerBase.GetService<IMediator>();

            return mediator.Publish(notification, cancellationToken);
        }
    }
}
