using DNI.ModuleLoader.Core.Base;
using DNI.Modules.Database.Abstractions;
using DNI.Shared.Abstractions;
using DNI.Shared.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace DNI.Modules.Database
{
    public class EntityFrameworkAppModule<TDbContext> : AppModuleBase<EntityFrameworkAppModule<TDbContext>>
        where TDbContext: DbContext
    {
        private readonly IEntityFrameworkAppConfig config;

        private static void ConfigureDbContext(IServiceProvider services, DbContextOptionsBuilder builder)
        {
            var entityFrameworkConfig = services.GetRequiredService<IEntityFrameworkAppConfig>();

            builder.UseSqlServer(entityFrameworkConfig.ConnectionString);
        }

        public EntityFrameworkAppModule(IAppModuleCache<EntityFrameworkAppModule<TDbContext>> appModuleCache,
            IEntityFrameworkAppConfig config) : base(appModuleCache)
        {
            this.config = config;
        }

        public static void RegisterConfig(IAppModuleConfig<EntityFrameworkAppModule<TDbContext>> appModuleConfig)
        {
            appModuleConfig.AddConfiguration<EntityFrameworkAppConfig>();
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IServiceCollection services)
        {
            services.AddDbContext<TDbContext>(ConfigureDbContext);
        }

        

        public override Task RunAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public override bool ValidateServices(IServiceProvider serviceProvider)
        {
            return true;
        }
    }
}
