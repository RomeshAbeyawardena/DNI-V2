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
        where TDbContext: DbContext, new()
    {
        private readonly IEntityFrameworkAppConfig config;

        public EntityFrameworkAppModule(IAppModuleCache<EntityFrameworkAppModule<TDbContext>> appModuleCache,
            IEntityFrameworkAppConfig config) : base(appModuleCache)
        {
            this.config = config;
        }

        public static void RegisterServices(IAppModuleCache appModuleCache, IAppModuleConfig<EntityFrameworkAppModule<TDbContext>> appModuleConfig, IServiceCollection services)
        {
            appModuleConfig.AddConfiguration<EntityFrameworkAppConfig>();
            services.AddSingleton<IEntityFrameworkAppConfig>(GetEntityFrameworkConfig);
            services.AddDbContext<TDbContext>();
        }

        private static IEntityFrameworkAppConfig GetEntityFrameworkConfig(IServiceProvider arg)
        {
            arg.GetRequiredService<IConf>()
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
