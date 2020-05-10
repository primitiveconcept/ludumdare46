namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;


    /// <summary>
    ///     Wraps a single Int64 value to easily manipulate IP addresses.
    /// </summary>
    [JsonConverter(typeof(IPJsonConverter))]
    public struct IP
    {
        public const uint PossiblePublicIPv4Addresses = 3706452992;
        private static readonly IP[] SourceAddressRange = { new IP("0.0.0.0"), new IP("0.255.255.255") };
        private static readonly IP[] LoopbackAddressRange = { new IP("127.0.0.0"), new IP("127.255.255.255") }; 
        private static readonly IP[] PrivateAddressRanges = 
            { 
                new IP("10.0.0.0"), new IP("10.255.255.255"), 
                new IP("100.64.0.0"), new IP("100.127.255.255"), 
                new IP("172.16.0.0"), new IP("172.31.255.255"), 
                new IP("192.0.0.0"), new IP("192.0.0.255"),
                new IP("192.168.0.0"), new IP("192.168.255.255"), 
                new IP("198.18.0.0"), new IP("198.19.255.255") 
            };
        private static readonly IP[] ReservedAddressRanges =
            {
                new IP("100.64.0.0"), new IP("100.127.255.255"),
                new IP("169.254.0.0"), new IP("169.254.255.255"),
                new IP("192.0.2.0"), new IP("192.0.2.255"),
                new IP("192.88.99.0"), new IP("192.88.99.255"),
                new IP("198.51.100.0"), new IP("198.51.100.255"), 
                new IP("203.0.113.0"), new IP("203.0.113.255"), 
                new IP("224.0.0.0"), new IP("239.255.255.255"),
                new IP("240.0.0.0"), new IP("255.255.255.254"),
                new IP("255.255.255.255"), new IP("255.255.255.255")  
            };

        private const string IPMatchPattern = @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?";

        public uint Value;


        public bool IsValid
        {
            get { return this.Value > -1; }
        }


        public bool IsPrivateAddress()
        {
            return IsInRange(PrivateAddressRanges);
        }


        public bool IsLoopbackAddress()
        {
            return IsInRange(LoopbackAddressRange);
        }


        public bool IsReservedAddress()
        {
            return IsInRange(ReservedAddressRanges);
        }


        public bool IsSourceAddress()
        {
            return IsInRange(SourceAddressRange);
        }

        public bool IsValidPublicAddress()
        {
            return !IsSourceAddress()
                   && !IsLoopbackAddress()
                   && !IsPrivateAddress()
                   && !IsReservedAddress();
        }
        

        public byte this[uint index]
        {
            get
            {
                string ipString = ToString();
                string[] parts = ipString.Split('.');
                return byte.Parse(parts[index]);
            }
            set
            {
                string ipString = ToString();
                string[] parts = ipString.Split('.');
                parts[index] = value.ToString();
                ipString = string.Join(separator: '.', value: parts);
                this.Value = ConvertToInt(ipString);
            }
        }


        public string NetworkPart
        {
            get { return $"{this[0]}.{this[1]}"; }
        }


        public string HostPart
        {
            get { return $"{this[2]}.{this[3]}"; }
        }


        public IP(string address)
        {
            this.Value = ConvertToInt(address);
        }


        public IP(uint value)
        {
            this.Value = value;
        }


        public IP(byte[] value)
        {
            this.Value = ConvertToInt(value);
        }


        public static implicit operator IP(string ip)
        {
            return new IP(ip);
        }


        public static implicit operator IP(uint ip)
        {
            return new IP(ip);
        }


        public static implicit operator string(IP ip)
        {
            return ConvertToString(ip.Value);
        }


        public static implicit operator long(IP ip)
        {
            return ip.Value;
        }


        public static uint ConvertToInt(byte[] ipBytes)
        {
            return BitConverter.ToUInt32(ipBytes, 0);
        }
        
        
        public static uint ConvertToInt(string ip)
        {
            IPAddress address = IPAddress.Parse(ip);
            byte[] bytes = address.GetAddressBytes();

            // flip big-endian(network order) to little-endian
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            return BitConverter.ToUInt32(bytes, 0);
        }


        public static string ConvertToString(uint ip)
        {
            byte[] bytes = BitConverter.GetBytes(ip);

            // flip little-endian to big-endian(network order)
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            
            return new IPAddress(bytes).ToString();
        }


        public override string ToString()
        {
            return ConvertToString(this.Value);
        }


        public static List<string> Extract(string value)
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


        public bool IsInRange(IP[] addressTuples)
        {
            for (uint i = 0; i < addressTuples.Length; i += 2)
            {
                if (IsInRange(addressTuples[i].Value, addressTuples[i + 1].Value)) 
                    return true;
            }

            return false;
        }
        
        
        public bool IsInRange(uint lower, uint upper)
        {
            return this.Value >= lower
                   && this.Value <= upper;
        }
    }


    public class IPJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IP).IsAssignableFrom(objectType);
        }


        public override object? ReadJson(
            JsonReader reader,
            Type objectType,
            object? existingValue,
            JsonSerializer serializer)
        {
            uint ipValue = (uint)reader.Value;
            IP ip = new IP(ipValue);
            return ip;
        }


        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            uint ipValue = ((IP)value).Value;
            writer.WriteValue(ipValue);
        }
    }
}