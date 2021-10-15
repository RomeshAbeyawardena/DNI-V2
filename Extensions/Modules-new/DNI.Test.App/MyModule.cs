using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class MyModule : ModuleBase
    {
        private readonly string greeting;
        private readonly IMyService myService;

        public MyModule(string greeting, IMyService myService)
        {
            this.greeting = greeting;
            this.myService = myService;
        }

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddSingleton("Hello")
                .AddSingleton<IMySharedClass>(new MySharedClass { Value = 123456 });
        }

        public override void Dispose(bool disposing)
        {
            
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            Console.WriteLine(greeting);
            Console.WriteLine(myService.MySharedClass.Value);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
