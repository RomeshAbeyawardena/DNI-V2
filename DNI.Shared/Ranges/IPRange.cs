using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Ranges
{
    public sealed class IpRange : Range<IPAddress>
    {
        private static uint GetUInt32(IPAddress iPAddress)
        {
            var addressBytes = iPAddress.GetAddressBytes().Reverse();
            return BitConverter.ToUInt32(addressBytes.ToArray());
        }
        public static bool IsGreaterThanOrEqual(IPAddress iPAddress, IPAddress otherIpAddress)
        {
            return GetUInt32(iPAddress) >= GetUInt32(otherIpAddress);
        }

        public static bool IsLessThanOrEqual(IPAddress iPAddress, IPAddress otherIpAddress)
        {
            return GetUInt32(iPAddress) <= GetUInt32(otherIpAddress);
        }

        public IpRange(IPAddress start, IPAddress end)
            : base(start, end, IsLessThanOrEqual, IsGreaterThanOrEqual)
        {

        }
    }
}
