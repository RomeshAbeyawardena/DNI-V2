using DNI.Mediator.Extensions;
using DNI.Tests.Shared.Models;
using DNI.Tests.Shared.Request;
using DNI.Web.Shared.Base;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.Core.Controller
{
    public class MyController : ApiControllerBase
    {
        public Task<IEnumerable<Customer>> Test(CancellationToken cancellationToken)
        {
            return this.Send(new TestRequest(), cancellationToken);
        }
    }
}
