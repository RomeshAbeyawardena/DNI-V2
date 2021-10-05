using DNI.Data.Extensions;
using DNI.Data.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Modules
{
    public class DbContextModule : ModuleBase
    {
        [Resolve]
        public static IDbContextModuleOptions ModuleOptions { get; }

        public static void ConfigureServices(IServiceCollection services)
        {
            foreach(var moduleOptionDbContext in ModuleOptions.DbContextTypeOptions)
            services.AddRepositoriesForDbContext(moduleOptionDbContext.Type, moduleOptionDbContext.DbContextOptionsBuilder, moduleOptionDbContext.ServiceLifetime);
        }

        public override Task OnRun(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task OnStop(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
