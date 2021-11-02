using DNI.Modules.Shared.Builders;
using Hangfire;
using System;

namespace DNI.Hangfire.Shared.Abstractions.Builder
{
    public interface IHangfireModuleOptionsBuilder : IModuleOptionsBuilder<IHangfireModuleOptions>
    {
        IHangfireModuleOptionsBuilder UseHangfireDashboard();
        IHangfireModuleOptionsBuilder ConfigureHangfire(Action<IGlobalConfiguration> configure);
        IHangfireModuleOptionsBuilder ConfigureOptions(string pathMatch,
            DashboardOptions dashboardOptions,
            JobStorage jobStorage);
    }
}
