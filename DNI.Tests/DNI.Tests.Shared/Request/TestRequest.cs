using DNI.Tests.Shared.Models;
using MediatR;
using System.Collections.Generic;

namespace DNI.Tests.Shared.Request
{
    public class TestRequest : IRequest<IEnumerable<Customer>>
    {
        
    }
}
