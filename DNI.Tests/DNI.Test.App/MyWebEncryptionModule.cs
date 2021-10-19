using DNI.Encryption.Extensions;
using DNI.Encryption.Modules.Extensions;
using DNI.Mediator.Modules.Extensions;
using DNI.Modules.Shared.Abstractions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
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
                .ConfigureMediatorModule(builder => builder.AddModuleAssemblies())
                .ConfigureWebModule<Program>(builder => builder.AddModuleAssemblies())
                .ConfigureEncryptionModule(builder => builder
                    .ImportConfiguration()
                    .ConfigureOptions(s => s.ImportConfiguration("SecurityProfiles/General")));
        }
    }
}
