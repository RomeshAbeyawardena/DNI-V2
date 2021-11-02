using DNI.Modules.Shared.Builders;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using System;

namespace DNI.Hangfire.Shared.Abstractions.Builder
{
    public interface IHangfireModuleOptionsBuilder : IModuleOptionsBuilder<IHangfireModuleOptions>
    {
        IHangfireModuleOptionsBuilder UseHangfireDashboard();
        IHangfireModuleOptionsBuilder SetParentType(Type type);
        IHangfireModuleOptionsBuilder ConfigureHangfire(Action<IGlobalConfiguration> configure);
        IHangfireModuleOptionsBuilder ConfigureWebHost(Action<IWebHostBuilder> configure);
        IHangfireModuleOptionsBuilder ConfigureOptions(string pathMatch,
            DashboardOptions dashboardOptions,
            JobStorage jobStorage);
    }
}
