using Hangfire;

namespace DNI.Hangfire.Shared.Abstractions
{
    public interface IJobActivator
    {
        JobActivator JobActivator { get; }
    }
}
