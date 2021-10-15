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
        private readonly MySharedClass mySharedClass;

        public MyModule(string greeting, MySharedClass mySharedClass)
        {
            this.greeting = greeting;
            this.mySharedClass = mySharedClass;
        }

        public override void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton("Hello");
        }

        public override void Dispose(bool disposing)
        {
            
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            Console.WriteLine(greeting);
            Console.WriteLine(mySharedClass.Value);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
