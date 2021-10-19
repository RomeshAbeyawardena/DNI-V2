using System;

namespace DNI.Shared.Abstractions
{
    public interface IClockProvider
    {
        DateTimeOffset GetDateTimeOffset(bool useUtc);
        DateTime GetDateTime(bool useUtc);
    }
}
