using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DNI.Shared.Abstractions;

namespace DNI.Shared
{
    public class IpRange : Range<IPAddress>
    {
        private static readonly IEnumerable<int> quantifiedScale = new[] { 8, 4, 2, 1 };

        private static long GetQuantifiedValue(IEnumerable<byte> values,
                                       IEnumerable<int> quantifiedScale)
        {
            var sum = 0;
            for (int i = 0; i < 4; i++)
            {
                sum += values.ElementAt(i) * quantifiedScale.ElementAt(i);
            }
            
            return sum;
        }

        public static bool IsGreaterThanOrEqual(IPAddress iPAddress, IPAddress otherIpAddress)
        {
            var addressCount = GetQuantifiedValue(iPAddress.GetAddressBytes(),
                                                 quantifiedScale);
            var otherAddressCount = GetQuantifiedValue(otherIpAddress.GetAddressBytes(),
                                                      quantifiedScale);

            return otherAddressCount >= addressCount;
        }

        public static bool IsLessThanOrEqual(IPAddress iPAddress, IPAddress otherIpAddress)
        {
            var addressCount = GetQuantifiedValue(iPAddress.GetAddressBytes(),
                                                 quantifiedScale);
            var otherAddressCount = GetQuantifiedValue(otherIpAddress.GetAddressBytes(),
                                                      quantifiedScale);

            return otherAddressCount <= addressCount;
        }

        public IpRange(IPAddress start, IPAddress end)
            : base(start, end, IsLessThanOrEqual, IsGreaterThanOrEqual)
        {

        }
    }

    public class Range<T> : IRange<T>
    {
        public static bool operator==(Range<T> source, Range<T> target)
        {
            return source.Equals(target);
        }

        public static bool operator !=(Range<T> source, Range<T> target)
        {
            return !source.Equals(target);
        }

        private readonly Func<T, T, bool> isLessThanOrEqual;
        private readonly Func<T, T, bool> isGreaterThanOrEqual;

        public Range(T start, T end, 
            Func<T, T, bool> isLessThanOrEqual, 
            Func<T, T, bool> isGreaterThanOrEqual)
        {
            Start = start;
            End = end;
            this.isLessThanOrEqual = isLessThanOrEqual;
            this.isGreaterThanOrEqual = isGreaterThanOrEqual;
        }

        public T Start { get; }

        public T End { get; }

        public bool IsInRange(T value)
        {
            return isGreaterThanOrEqual(value, Start) 
                && isLessThanOrEqual(value, End);
        }

        public bool Equals(IRange<T> value)
        {
            return value.Start.Equals(Start)
                && value.End.Equals(End);
        }

        public override bool Equals(object obj)
        {
            if (obj is IRange<T> range)
                return range.Equals(this);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Start, End);
        }

        public override string ToString()
        {
            return $"Start: {Start}, End: {End}";
        }

        bool IEquatable<T>.Equals(T other)
        {
            return Equals(other);
        }
    }
}
