using DNI.Hangfire.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Hangfire;

namespace DNI.Test.Modules
{
    public class MyWebModule2 : ModuleBase
    {
        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            
            moduleConfigurationBuilder
                .ConfigureWebModule<MyWebModule2>(options => options.AddModuleAssemblies().ConfigureWebHost(Configure))
                .ConfigureHangfireModule<MyWebModule2>(options => options
                    .UseHangfireDashboard()
                    .ConfigureOptions("/hangfire", new DashboardOptions(), null)
                    .ConfigureWebHost(c => c.ConfigureKestrel(a => a.Listen(IPAddress.Any, 6050))));
        }

        private void Configure(IWebHostBuilder obj)
        {
            obj.ConfigureKestrel(a => a.Listen(IPAddress.Any, 5050));
        }
    }
}
