using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Mediator.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Mediator.Modules
{
    public class MediatorModule : ModuleBase
    {
        [Resolve] public static IMediatorModuleOptions Options { get; }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
