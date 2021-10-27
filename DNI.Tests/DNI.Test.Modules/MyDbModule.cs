using DNI.Data.Modules;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using DNI.Mapper.Modules.Extensions;

using System;
using DNI.Encryption.Modules.Extensions;
using DNI.Encryption.Extensions;
using DNI.Test.Core;
using DNI.FluentValidation.Modules.Extensions;

namespace DNI.Test.Modules
{
    public class MyDbModule : ModuleBase
    {
        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
          
            moduleConfigurationBuilder
                .ConfigureDbContextModule(builder => builder
                    .AddDbContext<MyDbContext>(ConfigureDbBuilder, ServiceLifetime.Scoped))
                .ConfigureFluentValidation(c => c.AddModuleAssemblies())
                .ConfigureMapperModule(c => c.AddModuleAssemblies())
                .ConfigureEncryptionModule(builder => builder
                    .ImportConfiguration()
                    .ConfigureOptions(s => s.ImportConfiguration("SecurityProfiles/General")));
        }

        private void ConfigureDbBuilder(IServiceProvider serviceProvider, DbContextOptionsBuilder builder)
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            builder
                .UseSqlServer(configuration.GetConnectionString("default"));
        }
    }
}
