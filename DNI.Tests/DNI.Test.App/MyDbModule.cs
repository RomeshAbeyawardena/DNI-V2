using DNI.Data.Modules;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DNI.Mapper.Modules.Extensions;
using System.Threading.Tasks;
using System.Threading;
using DNI.Shared.Abstractions;
using DNI.Shared.Test;
using System;

namespace DNI.Test.App
{
    public class MyDbModule : ModuleBase
    {
        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder.ConfigureDbContextModule(builder => builder.AddDbContext<MyDbContext>(ConfigureDbBuilder, ServiceLifetime.Scoped))
                .ConfigureMapperModule(c => c.AddModuleAssemblies());
        }

        private void ConfigureDbBuilder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            builder
                .UseSqlServer(configuration.GetConnectionString("default"));
        }
    }
}
