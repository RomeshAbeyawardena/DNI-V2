using DNI.Mediator.Shared.Abstractions;
using DNI.Tests.Shared.Models;
using System;
using System.Collections.Generic;

namespace DNI.Tests.Shared.Request
{
    public class TestRequest : IRequest<IEnumerable<Customer>>
    {
        
    }

    public class TestSaveRequest: IRequest<Guid>
    {
        public Customer Customer { get; set; }
    }
}
