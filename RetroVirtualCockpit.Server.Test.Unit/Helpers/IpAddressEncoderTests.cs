using System;
using Xunit;
using RetroVirtualCockpit.Server.Helpers;
using Shouldly;

namespace RetroVirtualCockpit.Server.Test.Unit.Helpers
{
    public class IpAddressEncoderTests
    {
        [Theory]
        [InlineData("0.0.0.0", "AQGWMCSI")]
        [InlineData("1.1.1.1", "ARGXMDSJ")]
        [InlineData("22.33.44.55", "BWIXOOVP")]
        [InlineData("122.233.044.255", "HAUFOOHX")]
        [InlineData("022.33.044.005", "BWIXOOSN")]
        [InlineData("192.168.1.10", "MQQEMDSS")]
        [InlineData("255.255.255.255", "PFVLBRHX")]
        public void Encode_ShouldWorkOnValidIpAddresses(string ipAddress, string expected)
        {
            var result = IpAddressEncoder.Encode(ipAddress);

            result.ShouldNotBeNullOrEmpty();
            result.Length.ShouldBe(8);
            result.ShouldBe(expected);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("hello")]
        [InlineData("...")]
        [InlineData("a.b.c.d")]
        [InlineData("1.1.1")]
        [InlineData("2.2.2.2.2")]
        [InlineData("-1.2.2.2")]
        public void Encode_ShouldThrowExceptionWithInvalidValues(string invalid)
        {
            Should.Throw<ArgumentException>(() => IpAddressEncoder.Encode(invalid));
        }
    }
}
