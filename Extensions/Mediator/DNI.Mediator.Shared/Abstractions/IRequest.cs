using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Mediator.Shared.Abstractions
{
    public interface IRequest<T> : MediatR.IRequest<IResponse<T>>
    {
        
    }
}
