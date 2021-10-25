using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Tests.Shared.Extensions
{
    public static class StringExtensions
    {
        public static bool IsEmail(this string value)
        {
            return value.Contains("@") && value.Contains(".");
        }
    }
}
