using DNI.Hangfire.Shared.Abstractions;
using Hangfire;
using Microsoft.AspNetCore.Hosting;
using System;

namespace DNI.Hangfire.Core.Defaults
{
    public class DefaultHangfireModuleOptions : IHangfireModuleOptions
    {
        public bool UseHangfireDashboard { get; }

        public DefaultHangfireModuleOptions(bool useHangfireDashboard, Action<IGlobalConfiguration> configureHangfire, string pathMatch,
            DashboardOptions dashboardOptions, JobStorage jobStorage, Action<IWebHostBuilder> configureWebHost, Type parentType)
        {
            UseHangfireDashboard = useHangfireDashboard;
            ConfigureHangfire = configureHangfire;
            PathMatch = pathMatch;
            DashboardOptions = dashboardOptions;
            JobStorage = jobStorage;
            ConfigureWebHost = configureWebHost;
            ParentType = parentType;
        }

        public Action<IWebHostBuilder> ConfigureWebHost { get; }

        public Action<IGlobalConfiguration> ConfigureHangfire { get; }

        public string PathMatch { get; }

        public DashboardOptions DashboardOptions { get; }

        public JobStorage JobStorage { get; }

        public Type ParentType { get; }
    }
}
