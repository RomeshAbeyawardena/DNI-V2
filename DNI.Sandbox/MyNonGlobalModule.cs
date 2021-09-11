﻿using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using DNI.Shared.Abstractions.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    public class MyNonGlobalModule : AppModuleBase<MyNonGlobalModule>
    {
        public MyNonGlobalModule(IAppModuleCache<MyNonGlobalModule> appModuleCache) : base(appModuleCache)
        {
        }

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            var placeholderService = ServiceProvider.GetService<IPlaceholderService>();
            Console.WriteLine("MyNonGlobalModule running");
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public override bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }
    }
}