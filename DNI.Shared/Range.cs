using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using DNI.Shared.Abstractions;
using DNI.Shared.Ranges;

namespace DNI.Shared
{
    public static class Range
    {

        public static IRange<IPAddress> CreateIPRange(IPAddress start, IPAddress end)
        {
            return new IpRange(start, end);
        }

        public static IRange<long> CreateNumericRange(long start, long end)
        {
            return new NumericRange(start, end);
        }

    }

    public abstract class Range<T> : IRange<T>
    {
        public static bool operator ==(Range<T> source, Range<T> target)
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
            Debug.WriteLine("First check GTE:");
            var check1 = isGreaterThanOrEqual(value, Start);
            
            Debug.WriteLine("First check LTE: ");
            var check2 = isLessThanOrEqual(value, End);

            return check1 && check2;
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
