using DNI.Shared.Abstractions;
using DNI.Shared.Enumerations;
using DNI.Shared.Extensions;

namespace DNI.Extensions
{
    public static class ClockProviderExtensions
    {
        public static void UpdateValueMetaTags<T>(this IClockProvider clockProvider, T value, MetaAction metaAction)
        {
            value.UpdateMetaTags(metaAction, clockProvider.GetDateTimeOffset, clockProvider.GetDateTime);
        }
    }
}
