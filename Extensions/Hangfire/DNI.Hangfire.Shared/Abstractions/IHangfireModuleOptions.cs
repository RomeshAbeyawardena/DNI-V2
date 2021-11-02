using Hangfire;
using System;

namespace DNI.Hangfire.Shared.Abstractions
{
    public interface IHangfireModuleOptions
    {
        bool UseHangfireDashboard { get; }
        Action<IGlobalConfiguration> ConfigureHangfire { get; }
        string PathMatch { get; }
        DashboardOptions DashboardOptions { get; }
        JobStorage JobStorage { get; }
    }
}
