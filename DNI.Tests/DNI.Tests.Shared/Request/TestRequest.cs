using DNI.Mediator.Shared.Abstractions;
using DNI.Tests.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace DNI.Tests.Shared.Request
{
    public class TestRequest : IRequest<IResponse<IEnumerable<Customer>>>
    {
        
    }

    public class TestSaveRequest: IRequest<IResponse<Guid>>
    {
        public Customer Customer { get; set; }
    }
}
