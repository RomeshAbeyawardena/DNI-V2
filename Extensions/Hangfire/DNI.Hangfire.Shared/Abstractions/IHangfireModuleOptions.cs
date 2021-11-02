using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
