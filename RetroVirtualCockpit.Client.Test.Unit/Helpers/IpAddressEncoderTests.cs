using System;
using NUnit.Framework;
using RetroVirtualCockpit.Client.Helpers;
using Shouldly;

namespace RetroVirtualCockpit.Client.Test.Unit.Helpers
{
    [TestFixture]
    public class IpAddressEncoderTests
    {
        [Test]
        [TestCase("0.0.0.0", "AQGWMCSI")]
        [TestCase("1.1.1.1", "ARGXMDSJ")]
        [TestCase("22.33.44.55", "BWIXOOVP")]
        [TestCase("122.233.044.255", "HAUFOOHX")]
        [TestCase("022.33.044.005", "BWIXOOSN")]
        [TestCase("192.168.1.10", "MQQEMDSS")]
        [TestCase("255.255.255.255", "PFVLBRHX")]
        public void Encode_ShouldWorkOnValidIpAddresses(string ipAddress, string expected)
        {
            var result = IpAddressEncoder.Encode(ipAddress);

            result.ShouldNotBeNullOrEmpty();
            result.Length.ShouldBe(8);
            result.ShouldBe(expected);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("hello")]
        [TestCase("...")]
        [TestCase("a.b.c.d")]
        [TestCase("1.1.1")]
        [TestCase("2.2.2.2.2")]
        [TestCase("-1.2.2.2")]
        public void Encode_ShouldThrowExceptionWithInvalidValues(string invalid)
        {
            Should.Throw<ArgumentException>(() => IpAddressEncoder.Encode(invalid));
        }
    }
}
