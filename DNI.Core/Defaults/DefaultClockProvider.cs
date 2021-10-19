using DNI.Shared.Abstractions;
using DNI.Shared.Attributes;
using System;
using System.Reactive.PlatformServices;

namespace DNI.Core.Defaults
{
    [RegisterService]
    public class DefaultClockProvider : IClockProvider
    {
        private readonly ISystemClock systemClock;

        public DefaultClockProvider(ISystemClock systemClock)
        {
            this.systemClock = systemClock;
        }

        public DateTime GetDateTime(bool useUtc)
        {
            return useUtc 
                ? systemClock.UtcNow.DateTime 
                : systemClock.UtcNow.LocalDateTime;
        }

        public DateTimeOffset GetDateTimeOffset(bool useUtc)
        {
            return useUtc
                ? systemClock.UtcNow.UtcDateTime
                : systemClock.UtcNow;
        }
    }
}
