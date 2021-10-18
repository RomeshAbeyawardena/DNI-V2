using DNI.Web.Shared.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Extensions
{
    public static class ControllerBaseExtensions
    {
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
