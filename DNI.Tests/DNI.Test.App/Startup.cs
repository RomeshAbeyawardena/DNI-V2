using DNI.Shared.Abstractions;
using DNI.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class Startup : DisposableStartupBase
    {
        public override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
