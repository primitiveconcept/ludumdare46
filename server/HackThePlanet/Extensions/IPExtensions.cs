#pragma warning disable 0618

namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;


    public static class IPExtensions
    {
        public const string IPMatchPattern = @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?";

        public static long Generate()
        {
            // var worldWideNetwork = Game.GetComponents<ComputerComponent>();
            
            byte[] randomIpBytes = new byte[4];
            new Random().NextBytes(randomIpBytes);
            long ipAddress = new IPAddress(randomIpBytes).Address;
            Game.LogInfo(ipAddress.ToIPString());
            //if (worldWideNetwork.Any(computer => computer.IpAddress == ipAddress))
            //    return 0;

            return ipAddress;
        }

        public static List<string> FindIPs(this string value)
        {
            List<string> ips = new List<string>();

            Regex regex = new Regex(IPMatchPattern);
            Match match = regex.Match(value);

            while (match.Success)
            {
                ips.Add(match.Value);
                match = match.NextMatch();
            }

            return ips;
        }


        public static long ToIPLong(this string ip)
        {
            return IPAddress.Parse(ip).Address;
        }


        public static string ToIPString(this long ip)
        {
            return new IPAddress(ip).ToString();
        }
    }
}