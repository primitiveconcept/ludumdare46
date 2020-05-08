namespace HackThePlanet
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Text.RegularExpressions;
    using Newtonsoft.Json;


    /// <summary>
    ///     Wraps a single Int64 value to easily manipulate IP addresses.
    /// </summary>
    [JsonConverter(typeof(IPJsonConverter))]
    public struct IP
    {
        private const string IPMatchPattern = @"\d\d?\d?\.\d\d?\d?\.\d\d?\d?\.\d\d?\d?";

        public long Value;


        public bool IsValid
        {
            get { return this.Value > 0; }
        }

        public byte this[int index]
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
                this.Value = ConvertToLong(ipString);
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
            this.Value = ConvertToLong(address);
        }


        public IP(long value)
        {
            this.Value = value;
        }


        public IP(byte[] value)
        {
            this.Value = new IPAddress(value).Address;
        }


        public static implicit operator IP(string ip)
        {
            return new IP(ip);
        }


        public static implicit operator IP(long ip)
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


        public static long ConvertToLong(string ip)
        {
            IPAddress ipAddress;
            if (IPAddress.TryParse(ip, out ipAddress))
            {
                return ipAddress.Address;
            };

            return -1;
        }


        public static string ConvertToString(long ip)
        {
            return new IPAddress(ip).ToString();
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
            long ipValue = (long)reader.Value;
            IP ip = new IP(ipValue);
            return ip;
        }


        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            long ipValue = ((IP)value).Value;
            writer.WriteValue(ipValue);
        }
    }
}