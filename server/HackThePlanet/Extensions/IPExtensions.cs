#pragma warning disable 0618

namespace HackThePlanet
{
    using System.Net;


    public static class IPExtensions
    {
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