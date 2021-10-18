using DNI.Web.Shared.Base;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Base
{
    public abstract class MediatorApiControllerBase : ApiControllerBase
    {
        protected IMediator Mediator => GetService<IMediator>();

        [NonAction]
        protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            return Mediator.Send(request, cancellationToken);
        }

        [NonAction]
        protected Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        {
            return Mediator.Publish(notification, cancellationToken);
        }
    }

    public abstract class MediatorApiVersionControllerBase : ApiVersionControllerBase
    {
        protected IMediator Mediator => GetService<IMediator>();

        [NonAction]
        protected Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            return Mediator.Send(request, cancellationToken);
        }

        [NonAction]
        protected Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken)
        {
            return Mediator.Publish(notification, cancellationToken);
        }
    }
}
