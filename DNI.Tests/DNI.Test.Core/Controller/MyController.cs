using DNI.Mediator.Extensions;
using DNI.Tests.Shared.Models;
using DNI.Tests.Shared.Request;
using DNI.Web.Shared.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.Core.Controller
{
    public class MyController : ApiControllerBase
    {
        public async Task<IActionResult> Test(CancellationToken cancellationToken)
        {
            return this.Process(await this.Send(new TestRequest(), cancellationToken));
        }

        [Route("save")]
        public async Task<IActionResult> Test2(CancellationToken cancellationToken)
        {
            return this.Process(await this.Send(new TestSaveRequest(), cancellationToken));
        }
    }
}
