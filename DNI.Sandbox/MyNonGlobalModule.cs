using DNI.ModuleLoader.Core.Base;
using DNI.Shared.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using DNI.Shared.Abstractions.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using DNI.Extensions;
using DNI.Modules.Database;
using DNI.Sandbox.Entities;

namespace DNI.Sandbox
{
    public class MyNonGlobalModule : AppModuleBase<MyNonGlobalModule>
    {
        private IAsyncRepository<User> userRepository;

        public MyNonGlobalModule(IAppModuleCache<MyNonGlobalModule> appModuleCache) : base(appModuleCache)
        {

        }

        public static void RegisterServices(IAppModuleCache appModuleCache,  IServiceCollection services)
        {
            appModuleCache.RegisterModule<EntityFrameworkAppModule<SandboxDbContext>>(c => c.RegisterAssembly<MyNonGlobalModule>());
        }


        public override async Task RunAsync(CancellationToken cancellationToken)
        {
            var placeholderService = ServiceProvider.GetService<IPlaceholderService>();
            var result = await BeginScope<IAsyncRepository<User>, ValueTask<User>>(a => a.GetAsync(new User { Id = Guid.NewGuid() }, cancellationToken));
            Console.WriteLine("MyNonGlobalModule running");
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
