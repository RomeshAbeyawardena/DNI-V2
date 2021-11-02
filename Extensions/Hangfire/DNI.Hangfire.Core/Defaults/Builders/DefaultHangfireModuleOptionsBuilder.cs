using DNI.Hangfire.Shared.Abstractions;
using DNI.Hangfire.Shared.Abstractions.Builder;
using DNI.Modules.Shared.Base.Buillders;
using Hangfire;
using System;

namespace DNI.Hangfire.Core.Defaults.Builders
{
    public class DefaultHangfireModuleOptionsBuilder : ModuleOptionsBuilderBase<IHangfireModuleOptions>,
        IHangfireModuleOptionsBuilder
    {
        private bool useHangfireDashboard;
        private Action<IGlobalConfiguration> configureHangfire;
        private string pathMatch;
        private DashboardOptions dashboardOptions;
        private JobStorage jobStorage;

        public override IHangfireModuleOptions Build()
        {
            return new DefaultHangfireModuleOptions(useHangfireDashboard, configureHangfire, pathMatch, dashboardOptions, jobStorage);
        }

        public IHangfireModuleOptionsBuilder ConfigureHangfire(Action<IGlobalConfiguration> configure)
        {
            this.configureHangfire = configure;
            return this;
        }

        public IHangfireModuleOptionsBuilder ConfigureOptions(string pathMatch,
            DashboardOptions dashboardOptions, JobStorage jobStorage)
        {
            this.pathMatch = pathMatch;
            this.dashboardOptions = dashboardOptions;
            this.jobStorage = jobStorage;
            return this;
        }

        public IHangfireModuleOptionsBuilder UseHangfireDashboard()
        {
            useHangfireDashboard = true;
            return this;
        }
    }
}
