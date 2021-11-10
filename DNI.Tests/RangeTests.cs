using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DNI.Tests
{
    public class RangeTests
    {
        [TestCase("1.0.0.1", "1.0.0.254", "1.0.0.2", true)]
        [TestCase("1.0.0.1", "1.0.0.254", "1.0.0.18", true)]
        [TestCase("1.0.0.1", "1.0.0.254", "1.0.0.38", true)]
        [TestCase("1.0.0.1", "1.0.0.254", "1.0.0.254", true)]
        [TestCase("1.0.0.1", "1.0.0.254", "1.0.1.1", false)]
        [TestCase("192.168.0.1", "192.168.1.254", "1.0.1.1", false)]
        public void IPRangeTest(string ipAddressVal, string otherIpAddressVal, string expectedIpAddressVal, bool isValid)
        {
            var ipAddress = IPAddress.Parse(ipAddressVal);
            var otherIpAddress = IPAddress.Parse(otherIpAddressVal);
            var expectedIpAddress = IPAddress.Parse(expectedIpAddressVal);
            var ipRange = Shared.Range.CreateIPRange(ipAddress, otherIpAddress);
            Assert.AreEqual(isValid, ipRange.IsInRange(expectedIpAddress));
        }
    }
}
