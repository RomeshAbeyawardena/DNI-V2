using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Abstractions
{
    public interface IPayloadOptions
    {
        char HeaderSeparator { get; }
        char ParameterSeparator { get; }
    }
}
