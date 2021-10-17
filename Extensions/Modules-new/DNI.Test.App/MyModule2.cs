﻿using DNI.Modules.Shared.Abstractions;
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

        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            serviceCollection
                .AddSingleton<IMyService, MyService>();
        }

        public override void OnDispose(bool disposing)
        {
            
        }

        public override Task OnStart(CancellationToken cancellationToken)
        {
            Console.WriteLine(greeting);
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}