using DNI.Hangfire.Modules.Extensions;
using DNI.Modules.Shared.Abstractions.Builders;
using DNI.Modules.Shared.Base;
using DNI.Web.Modules.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using Hangfire;
using System;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;

namespace DNI.Test.Modules
{
    public class MyWebModule2 : ModuleBase
    {
        private readonly IConfiguration configuration;

        public MyWebModule2(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public override void ConfigureModuleBuilder(IServiceCollection services, IModuleConfigurationBuilder moduleConfigurationBuilder)
        {
            
            moduleConfigurationBuilder
                .ConfigureWebModule<MyWebModule2>(options => options.AddModuleAssemblies().ConfigureWebHost(Configure))
                .ConfigureHangfireModule<MyWebModule2>(options => options
                    .UseHangfireDashboard()
                    .ConfigureOptions("/hangfire", new DashboardOptions(), null)
                    .ConfigureHangfire(Configure)
                    .ConfigureWebHost(ConfigureHangFire));
        }

        private void Configure(IGlobalConfiguration configuration)
        {
            configuration.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(this.configuration.GetConnectionString("hangfire"), new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true
            });
        }

        private void ConfigureHangFire(IWebHostBuilder webHostBuilder)
        {
            webHostBuilder.ConfigureKestrel(a => a.Listen(IPAddress.Any, 5060));
        }

        private void Configure(IWebHostBuilder obj)
        {
            obj.ConfigureKestrel(a => a.Listen(IPAddress.Any, 5050));
        }
    }
}
