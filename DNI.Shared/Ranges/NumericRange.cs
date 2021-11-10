using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Ranges
{
    public sealed class NumericRange : Range<long>
    {
        public static bool IsLessThanOrEqual(long number, long otherNumber)
        {
            return number <= otherNumber;
        }


        public static bool IsGreaterThanOrEqual(long number, long otherNumber)
        {
            return number >= otherNumber;
        }

        public NumericRange(long start, long end)
            : base(start, end, IsLessThanOrEqual, IsGreaterThanOrEqual)
        {

        }
    }
}
