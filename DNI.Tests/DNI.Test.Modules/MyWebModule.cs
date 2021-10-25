using DNI.Mediator.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Shared.Attributes;
using DNI.Test.Core;
using DNI.Web.Modules.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DNI.Test.Modules
{
    [RequiresDependencies(typeof(MyDbContext))]
    public class MyWebModule : ModuleBase
    {
        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            moduleConfigurationBuilder
                .ConfigureWebModule<MyWebModule>(builder => builder
                    .AddAssembly<MyDbContext>())
                .ConfigureMediatorModule(builder => builder.AddModuleAssemblies());
        }
    }
}
