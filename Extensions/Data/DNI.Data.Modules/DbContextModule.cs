using DNI.Data.Extensions;
using DNI.Data.Shared.Abstractions;
using DNI.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Data.Modules
{
    public class DbContextModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection services, IModuleConfiguration moduleConfiguration)
        {
            services.AddRequiredServices();

            foreach (var moduleOptionDbContext in moduleConfiguration.GetOptions<IDbContextModuleOptions>().DbContextTypeOptions)
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

        public override void OnDispose(bool disposing)
        {

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
