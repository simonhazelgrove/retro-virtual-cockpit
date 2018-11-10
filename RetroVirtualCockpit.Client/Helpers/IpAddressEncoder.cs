using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace RetroVirtualCockpit.Client.Helpers
{
    public static class IpAddressEncoder
    {
        private const int LettersInAlphabet = 26;

        private const int MaxHex = 16;

        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string Encode(string ipAddress)
        {
            if (!ValidateIpAddress(ipAddress))
            {
                throw new ArgumentException("ipAddress parameter should be in format 'xxx.xxx.xxx.xxx'");
            }

            var hex = ToHex(ipAddress);
            var encoded = string.Empty;
            var codex = string.Empty;

            while (hex.Length > 0)
            {
                if (codex.Length < LettersInAlphabet)
                {
                    codex += Alphabet;
                }

                var index = int.Parse(hex[0].ToString(), System.Globalization.NumberStyles.HexNumber);
                encoded += codex[index];

                hex = hex.Substring(1);
                codex = codex.Substring(MaxHex);
            }

            return encoded;
        }

        private static bool ValidateIpAddress(string ipAddress)
        {
            Match match = Regex.Match(ipAddress, @"^[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}$");
            return match.Success;
        }

        private static string ToHex(string ipAddress)
        {
            var hexStrings = ipAddress.Split(new[] {"."}, StringSplitOptions.RemoveEmptyEntries)
                                      .Select(s => int.Parse(s).ToString("X2"));

            var hexString = string.Join(string.Empty, hexStrings);

            return hexString;
        }
    }
}
