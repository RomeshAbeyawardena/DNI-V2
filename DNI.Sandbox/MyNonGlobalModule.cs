using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using DNI.Shared.Abstractions.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using DNI.Extensions;
using DNI.Modules.Database;
using Microsoft.EntityFrameworkCore;

namespace DNI.Sandbox
{
    public class MyNonGlobalModule : AppModuleBase<MyNonGlobalModule>
    {
        private readonly SandboxDbContext dbContext;

        public MyNonGlobalModule(IAppModuleCache<MyNonGlobalModule> appModuleCache, SandboxDbContext dbContext) : base(appModuleCache)
        {
            this.dbContext = dbContext;
        }

        public static void RegisterServices(IAppModuleCache appModuleCache,  IServiceCollection services)
        {
            appModuleCache.RegisterModule<EntityFrameworkAppModule<SandboxDbContext>>();
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

    public class SandboxDbContext : DbContext
    {
        public SandboxDbContext(DbContextOptions options)
            : base(options)
        {

        }
    }
}
