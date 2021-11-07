using DNI.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Shared.Abstractions
{
    public interface IWebPortManager
    {
        IRange<IPAddress> IpAddressRange { get; }
        IPEndPoint GetNextAvailableIPEndPoint();
    }
}
