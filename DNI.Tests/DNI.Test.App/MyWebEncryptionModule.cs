using DNI.Data.Modules;
using DNI.Encryption.Extensions;
using DNI.Encryption.Modules.Extensions;
using DNI.Mediator.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Test.App
{
    public class MyWebEncryptionModule : ModuleBase
    {
        public override void ConfigureServices(IServiceCollection serviceCollection, IModuleConfiguration moduleConfiguration)
        {

        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureDbContextModule(builder => builder
                    .AddDbContext<MyDbContext>((s, b) => b.UseSqlServer(s
                        .GetService<IConfiguration>()
                        .GetConnectionString("default")), ServiceLifetime.Scoped))
                .ConfigureMediatorModule(builder => builder.AddModuleAssemblies())
                .ConfigureWebModule<Program>(builder => builder.AddModuleAssemblies())
                .ConfigureEncryptionModule(builder => builder
                    .ImportConfiguration()
                    .ConfigureOptions(s => s.ImportConfiguration("SecurityProfiles/General")));
        }
    }
}
