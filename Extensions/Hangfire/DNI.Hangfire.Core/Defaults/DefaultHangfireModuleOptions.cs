using DNI.Hangfire.Shared.Abstractions;
using Hangfire;
using System;

namespace DNI.Hangfire.Core.Defaults
{
    public class DefaultHangfireModuleOptions : IHangfireModuleOptions
    {
        public bool UseHangfireDashboard { get; }

        public DefaultHangfireModuleOptions(bool useHangfireDashboard, Action<IGlobalConfiguration> configureHangfire, string pathMatch, DashboardOptions dashboardOptions, JobStorage jobStorage)
        {
            UseHangfireDashboard = useHangfireDashboard;
            ConfigureHangfire = configureHangfire;
            PathMatch = pathMatch;
            DashboardOptions = dashboardOptions;
            JobStorage = jobStorage;
        }

        public Action<IGlobalConfiguration> ConfigureHangfire { get; }

        public string PathMatch { get; }

        public DashboardOptions DashboardOptions { get; }

        public JobStorage JobStorage { get; }
    }
}
