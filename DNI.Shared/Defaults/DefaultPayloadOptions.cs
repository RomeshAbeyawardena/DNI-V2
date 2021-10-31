using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Defaults
{
    public class DefaultPayloadOptions : IPayloadOptions
    {
        public static IPayloadOptions Default => new DefaultPayloadOptions
        {
            HeaderSeparator = ':',
            ParameterSeparator = '|'
        };

        public char HeaderSeparator { get; init; }
        public char ParameterSeparator { get; init; }
    }
}
