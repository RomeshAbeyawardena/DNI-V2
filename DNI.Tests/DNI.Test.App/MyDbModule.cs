using DNI.Data.Modules;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Test.App
{
    public class MyDbModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {
            
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder.ConfigureDbContextModule(builder => builder
                    .AddDbContext<MyDbContext>((s, b) => b.UseSqlServer(s
                        .GetService<IConfiguration>()
                        .GetConnectionString("default")), ServiceLifetime.Scoped));
        }
    }
}
