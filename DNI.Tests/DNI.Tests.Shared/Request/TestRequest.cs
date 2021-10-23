using DNI.Tests.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Tests.Shared.Request
{
    public class TestRequest : IRequest<IEnumerable<Customer>>
    {
        
    }
}
