using DNI.Data.Extensions;
using DNI.Data.Shared.Abstractions;
using DNI.Modules.Shared.Attributes;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Modules
{
    [RequiresDependencies(typeof(Core.This))]
    public class DbContextModule : ModuleBase
    {
        [Resolve] private static IDbContextModuleOptions ModuleOptions { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddRequiredServices();

            foreach (var moduleOptionDbContext in ModuleOptions.DbContextTypeOptions)
            {
                if (moduleOptionDbContext.DbContextOptionsBuilder != null)
                {
                    services.AddRepositoriesForDbContext(moduleOptionDbContext.Type,
                        moduleOptionDbContext.DbContextOptionsBuilder,
                        moduleOptionDbContext.ServiceLifetime);
                }

                if (moduleOptionDbContext.DbContextOptionsFactoryBuilder != null)
                {
                    services.AddRepositoriesForDbContext(moduleOptionDbContext.Type,
                        moduleOptionDbContext.DbContextOptionsFactoryBuilder,
                        moduleOptionDbContext.ServiceLifetime);
                }
            }
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
