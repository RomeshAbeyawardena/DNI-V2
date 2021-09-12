using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Shared.Configuration
{
    public class PlaceholderServiceConfig : IPlaceholderServiceConfig
    {
        public bool ThrowOnHandledExceptions { get; set; }
    }
}
