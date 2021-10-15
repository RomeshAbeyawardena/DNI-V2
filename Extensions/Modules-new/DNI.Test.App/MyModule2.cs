using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class MyModule2 : ModuleBase
    {
        private readonly string greeting;

        public MyModule2(string greeting)
        {
            this.greeting = greeting;
        }

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<MySharedClass>(new MySharedClass { Value = 123456 });
        }

        public override void Dispose(bool disposing)
        {
            throw new NotImplementedException();
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
