using DNI.Hangfire.Shared.Abstractions;
using Hangfire;
using System;

namespace DNI.Hangfire.Core.Defaults
{
    public class DefaultJobActivator : JobActivator, IJobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public JobActivator JobActivator => this;

        public DefaultJobActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type jobType)
        {
            return serviceProvider.GetService(jobType);
        }

        public override JobActivatorScope BeginScope(JobActivatorContext context)
        {
            return base.BeginScope(context);
        }
    }
}
