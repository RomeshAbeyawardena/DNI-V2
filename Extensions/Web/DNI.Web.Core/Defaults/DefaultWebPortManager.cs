using DNI.Shared.Abstractions;
using DNI.Web.Shared.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Web.Core.Defaults
{
    public class DefaultWebPortManager : IWebPortManager
    {
        public IRange<IPAddress> IpAddressRange { get; }
        public IRange<int> PortRange { get; }

        public IPEndPoint GetNextAvailableIPEndPoint()
        {
            throw new NotImplementedException();
        }
    }
}
