using DNI.Extensions;
using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Sandbox
{
    public class SandboxService { }

    public class SandboxModule : AppModuleBase<SandboxModule>
    {
        private readonly IFileProvider fileProvider;

        public SandboxModule(IFileProvider fileProvider, IAppModuleCache<SandboxModule> appModuleCache)
            : base(appModuleCache)
        {
            this.fileProvider = fileProvider;
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            appModuleCache.RegisterModule<MyNonGlobalModule>();
            services.AddSingleton<SandboxService>();
        }

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("SandboxModule running");
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
