using DNI.Mediator.Shared.Abstractions;
using DNI.Web.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using ControllerBase = DNI.Web.Shared.Base.ControllerBase;

namespace DNI.Mediator.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult Process(this ControllerBase controllerBase, IResponse response)
        {
            if (response.Succeeded)
            {
                return controllerBase.Ok(response.Result);
            }

            return controllerBase.BadRequest(response.Exception.Message);
        }

        public static Task<TResponse> Send<TResponse>(this ControllerBase controllerBase, IRequest<TResponse> request, CancellationToken cancellationToken)
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
